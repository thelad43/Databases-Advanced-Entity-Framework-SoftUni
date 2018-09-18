namespace Employees.App.Interfaces
{
    public interface ICommandParser
    {
        IExecutable ParseCommand(string[] data);
    }
}