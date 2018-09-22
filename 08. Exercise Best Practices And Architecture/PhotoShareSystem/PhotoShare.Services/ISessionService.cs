namespace PhotoShare.Services
{
    using Models;

    public interface ISessionService
    {
        User User { get; }

        User Login(string username, string password);

        void Logout();

        bool IsLoggedIn();
    }
}