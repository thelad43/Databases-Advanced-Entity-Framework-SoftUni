namespace BusTicketsSystem.App.Interfaces
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandName);
    }
}