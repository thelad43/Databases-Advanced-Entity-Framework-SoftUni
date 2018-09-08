namespace Minions.Services.Implementations
{
    using Data;
    using Data.Infrastructure;
    using Minions.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class MinionService : IMinionService
    {
        private readonly MinionsDbContext db;

        public MinionService(MinionsDbContext db)
        {
            this.db = db;
        }

        public void Add(Minion minion, Villain villain)
        {
            ValidateInput(minion.Name, minion.Name, villain.Name);

            AddTownIfDoesntExist(minion.Town.Name);

            AddVillainIfDoesntExists(villain.Name);

            villain
                .Minions
                .Add(new MinionsVillains { Minion = minion, Villain = villain });

            this.db.SaveChanges();

            Console.WriteLine($"Successfully added {minion.Name} to be minion of {villain.Name}.");
        }

        private void AddVillainIfDoesntExists(string villainName)
        {
            var villain = this.db.Villains.FirstOrDefault(v => v.Name == villainName);

            if (villain == null)
            {
                villain = new Villain
                {
                    Name = villainName,
                    EvilnessFactor = new EvilnessFactor { Name = villainName }
                };

                this.db.Add(villain);
                Console.WriteLine($"Villain {villain.Name} was added to the database.");
            }
        }

        private void AddTownIfDoesntExist(string townName)
        {
            var town = this.db.Towns.FirstOrDefault(t => t.Name == townName);

            if (town == null)
            {
                town = new Town
                {
                    Name = townName,
                    CountryId = 4
                    // countryId is always set with magic number 4 because in the input
                    // there is no country and throws exception after saving changes
                };

                this.db.Add(town);
                Console.WriteLine($"Town {town.Name} was added to the database.");
            }
        }

        private static void ValidateInput(string minionName, string town, string villainName)
        {
            Validator.ThrowExceptionIfNullOrWhitespace(minionName);
            Validator.ThrowExceptionIfNullOrWhitespace(town);
            Validator.ThrowExceptionIfNullOrWhitespace(villainName);
        }

        public int RemoveVillain(int id)
        {
            var villain = this.db
                .Villains
                .Where(v => v.Id == id)
                .Select(v => new
                {
                    v.Name,
                    v.Minions
                })
                .FirstOrDefault();

            if (villain == null)
            {
                throw new InvalidOperationException("No such villain was found.");
            }

            var removedMinions = villain.Minions.Count;

            var villainForRemoving = this.db.Villains.Find(id);

            this.db.Remove(villainForRemoving);

            this.db.SaveChanges();

            Console.WriteLine($"{villain.Name} was deleted.");

            return removedMinions;
        }

        public void PrintAllMinionNames()
        {
            var names = this.db.Minions.Select(m => m.Name).ToList();

            for (int first = 0, last = names.Count - 1; first <= last; first++, last--)
            {
                Console.WriteLine(names[first]);

                if (first != last)
                {
                    Console.WriteLine(names[last]);
                }
            }
        }

        public void IncreaseMinionsAge(IEnumerable<int> ids)
        {
            var minions = this.db
                .Minions
                .Where(m => ids.Contains(m.Id))
                .ToList();

            for (var i = 0; i < minions.Count; i++)
            {
                minions[i].Age++;
            }

            this.db.SaveChanges();
        }

        public void MakeNameTitleCase(IEnumerable<int> ids)
        {
            var minions = this.db
                .Minions
                .Where(m => ids.Contains(m.Id))
                .ToList();

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            for (var i = 0; i < minions.Count; i++)
            {
                minions[i].Name = textInfo.ToTitleCase(minions[i].Name);
            }

            this.db.SaveChanges();
        }

        public void PrintMinions()
        {
            var minions = this.db
                .Minions
                .Select(m => new
                {
                    m.Name,
                    m.Age
                })
                .ToList();

            foreach (var minion in minions)
            {
                Console.WriteLine($"{minion.Name} {minion.Age}");
            }
        }
    }
}