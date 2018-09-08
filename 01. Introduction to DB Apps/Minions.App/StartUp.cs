namespace Minions.App
{
    using Data;
    using Data.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Services;
    using Services.Implementations;
    using System;
    using System.Linq;

    public class StartUp
    {
        private static IServiceProvider serviceProvider;

        public static void Main()
        {
            serviceProvider = ConfigureServices();

            // Problem 02. Villain Names
            // Problem02();

            // Problem 03. Minion Names
            // Problem03();

            // Problem 04. Add Minion
            // Problem04();

            // Problem 05. Change Town Names Casing
            // Problem05();

            // Problem 06. Remove Villain
            // Problem06();

            // Problem 07. Print All Minion Names
            // minionsSerivce.PrintAllMinionNames();

            // Problem 08. Increase Minion Age
            // Problem08();
        }

        private static void Problem02()
        {
            var villainService = serviceProvider.GetService<IVillainService>();
            var villains = villainService.VillainsAndNumberOfMinions();

            foreach (var villain in villains)
            {
                Console.WriteLine($"{villain.Name} - {villain.MinionsCount}");
            }
        }

        private static void Problem03()
        {
            var villainService = serviceProvider.GetService<IVillainService>();

            var id = int.Parse(Console.ReadLine());

            try
            {
                var villainWithMinions = villainService.VillainWithMinionsById(id);

                Console.WriteLine($"Villain: {villainWithMinions.VillainName}");

                var i = 1;

                var minions = villainWithMinions.Minions;
                var hasMinions = minions.Any();

                if (!hasMinions)
                {
                    Console.WriteLine("(no minions)");
                }

                foreach (var minion in minions)
                {
                    Console.WriteLine($"{i++}. {minion.Name} {minion.Age}");
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Problem04()
        {
            // Minion: <Name> <Age> <TownName>
            var parts = Console.ReadLine().Split();

            var minionName = parts[1];
            var age = int.Parse(parts[2]);
            var townAsString = parts[3];

            var town = new Town { Name = townAsString };

            var minion = new Minion
            {
                Name = minionName,
                Age = age,
                Town = town
            };

            // Villain: <Name>
            var villainName = Console.ReadLine().Split().LastOrDefault();

            var villain = new Villain
            {
                Name = villainName
            };

            var minionService = serviceProvider.GetService<IMinionService>();

            minionService.Add(minion, villain);
        }

        private static void Problem05()
        {
            var townService = serviceProvider.GetService<ITownService>();

            var countryName = Console.ReadLine();
            var changedTowns = townService.ChangeTownNamesToUppercase(countryName);

            if (changedTowns == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{changedTowns} town names were affected.");

                var towns = townService.TownsByCountryName(countryName);

                Console.Write("[");

                Console.WriteLine(string.Join(", ", towns.Select(t => t.Name)) + "]");
            }
        }

        private static void Problem06()
        {
            var minionService = serviceProvider.GetService<IMinionService>();

            var id = int.Parse(Console.ReadLine());

            try
            {
                var removedMinions = minionService.RemoveVillain(id);

                Console.WriteLine($"{removedMinions} minions were released.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Problem08()
        {
            var minionService = serviceProvider.GetService<IMinionService>();

            var ids = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            minionService.IncreaseMinionsAge(ids);
            minionService.MakeNameTitleCase(ids);
            minionService.PrintMinions();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<MinionsDbContext>(options =>
                options.UseSqlServer(Configuration.ConnectionString));

            services.AddTransient<IMinionService, MinionService>();

            services.AddTransient<IVillainService, VillainService>();

            services.AddTransient<ITownService, TownService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}