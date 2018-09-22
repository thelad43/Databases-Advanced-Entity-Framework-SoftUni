namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using Services;

    using static Common.SuccessMessages;

    public class LogoutCommand : ICommand
    {
        private readonly ISessionService session;

        public LogoutCommand(ISessionService session)
        {
            this.session = session;
        }

        // Logout
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var username = this.session.User.Username;

            this.session.Logout();

            return string.Format(SuccessLogoutMessage, username);
        }
    }
}