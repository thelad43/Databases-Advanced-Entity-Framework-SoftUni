namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.Services;
    using System;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService users;

        public RegisterUserCommand(IUserService users)
        {
            this.users = users;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            var username = data[1];
            var password = data[2];
            var repeatPassword = data[3];
            var email = data[4];

            if (this.users.Exists(username))
            {
                throw new InvalidOperationException(UsernameIsTakenExceptionMessage);
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException(PasswordsDoNotMatchExceptionMessage);
            }

            this.users.Register(username, password, email);

            return string.Format(SuccessRegisterMessage, username);
        }
    }
}