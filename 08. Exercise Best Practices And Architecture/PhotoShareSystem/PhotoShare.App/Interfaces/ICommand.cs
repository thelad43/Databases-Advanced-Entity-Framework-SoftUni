namespace PhotoShare.App.Interfaces
{
    public interface ICommand
    {
        string Execute(string[] data);
    }
}