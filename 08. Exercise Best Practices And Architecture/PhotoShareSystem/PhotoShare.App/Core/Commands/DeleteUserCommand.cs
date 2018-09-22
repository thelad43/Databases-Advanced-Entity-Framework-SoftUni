namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using PhotoShare.Models;
    using Services;
    using System;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService users;
        private readonly ISessionService session;

        public DeleteUserCommand(IUserService users, ISessionService session)
        {
            this.users = users;
            this.session = session;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var username = data[1];

            var exists = this.users.Exists(username);

            if (!exists)
            {
                throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username));
            }

            var user = this.users.ByUsername<User>(username);

            if (user.IsDeleted.Value)
            {
                throw new InvalidOperationException(string.Format(UserIsAlreadyDeletedExceptionMessage, username));
            }

            if (this.session.User.Id != user.Id)
            {
                throw new InvalidOperationException(InvalidCredentialsExceptionMessage);
            }

            this.users.Delete(username);

            return string.Format(SuccessDeleteUserMessage, username);
        }
    }
}