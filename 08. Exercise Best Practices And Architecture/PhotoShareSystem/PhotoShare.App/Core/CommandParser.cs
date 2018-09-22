namespace PhotoShare.App.Core
{
    using Interfaces;
    using System;
    using System.Linq;
    using System.Reflection;

    using static Common.GlobalConstants;

    public class CommandParser : ICommandParser
    {
        private readonly IServiceProvider serviceProvider;

        public CommandParser(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string[] data)
        {
            var commandName = data[0].ToLower();

            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name.ToLower() == $"{commandName}{CommandSuffix}");

            if (commandType == null)
            {
                throw new InvalidOperationException($"Invalid command {commandName}!");
            }

            var command = InjectServices(commandType);

            return command;
        }

        private ICommand InjectServices(Type type)
        {
            var constructor = type.GetConstructors().First();

            var constructorParameters = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(this.serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)constructor.Invoke(services);

            return command;
        }
    }
}