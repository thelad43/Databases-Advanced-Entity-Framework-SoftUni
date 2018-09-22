namespace PhotoShare.App.Core
{
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var initializeService = this.serviceProvider.GetService<IDatabaseInitializerService>();

            initializeService.InitializeDatabase();

            var commandParser = this.serviceProvider.GetService<ICommandParser>();

            while (true)
            {
                Console.Write("Enter command: ");

                var line = Console.ReadLine();

                var arguments = line.Split();

                var result = string.Empty;

                try
                {
                    var command = commandParser.ParseCommand(arguments);
                    result = command.Execute(arguments);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine(result);
            }
        }
    }
}