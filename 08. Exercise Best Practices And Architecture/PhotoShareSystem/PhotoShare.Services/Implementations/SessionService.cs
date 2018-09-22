namespace PhotoShare.Services.Implementations
{
    using Models;
    using Services;

    public class SessionService : ISessionService
    {
        private readonly IUserService users;

        public SessionService(IUserService users)
        {
            this.users = users;
        }

        public User User { get; private set; }

        public User Login(string username, string password)
        {
            this.User = this.users.ByUsernameAndPassword<User>(username, password);

            return this.User;
        }

        public bool IsLoggedIn() => this.User != null;

        public void Logout() => this.User = null;
    }
}