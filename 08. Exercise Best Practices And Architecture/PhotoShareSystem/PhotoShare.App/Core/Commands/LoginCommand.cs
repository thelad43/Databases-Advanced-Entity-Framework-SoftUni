namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using Services;
    using System;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class LoginCommand : ICommand
    {
        private readonly ISessionService session;

        public LoginCommand(ISessionService session)
        {
            this.session = session;
        }

        // Login <username> <password>
        public string Execute(string[] data)
        {
            var username = data[1];
            var password = data[2];

            Validator.ThrowExceptionIfUserIsLoggedIn(this.session);

            var user = this.session.Login(username, password);

            if (user == null)
            {
                throw new ArgumentException(InvalidUsernameOrPasswordExceptionMessage);
            }

            return string.Format(SuccessLoginMessage, username);
        }
    }
}