namespace BusTicketsSystem.App.Core.Commands
{
    using Interfaces;
    using System;

    public class ExitCommand : ICommand
    {
        // Exit
        public string Execute(params string[] arguments)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}