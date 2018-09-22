namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using System;

    public class ExitCommand : ICommand
    {
        // Exit
        public string Execute(string[] data)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}