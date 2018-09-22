namespace PhotoShare.App.Interfaces
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string[] data);
    }
}