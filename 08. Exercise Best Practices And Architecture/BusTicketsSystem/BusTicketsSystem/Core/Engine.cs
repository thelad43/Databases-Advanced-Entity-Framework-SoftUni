namespace BusTicketsSystem.App.Core
{
    using Interfaces;
    using System;
    using System.Linq;

    public class Engine : IRunnable
    {
        private readonly CommandParser commandParser;

        public Engine(CommandParser commandParser)
        {
            this.commandParser = commandParser;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("Enter command: ");

                var arguments = Console.ReadLine().Split();

                var result = string.Empty;

                try
                {
                    var command = this.commandParser.ParseCommand(arguments.First());
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