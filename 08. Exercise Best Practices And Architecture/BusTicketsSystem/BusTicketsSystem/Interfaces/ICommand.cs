namespace BusTicketsSystem.App.Interfaces
{
    public interface ICommand
    {
        string Execute(params string[] arguments);
    }
}