namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using PhotoShare.Models;
    using Services;
    using System;
    using System.Linq;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService users;
        private readonly ITownService towns;
        private readonly ISessionService session;

        public ModifyUserCommand(IUserService users, ITownService towns, ISessionService session)
        {
            this.users = users;
            this.towns = towns;
            this.session = session;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var username = data[1];
            var property = data[2].ToLower();
            var newValue = data[3];

            var userId = this.users.ByUsername<User>(username).Id;

            if (userId == 0)
            {
                throw new ArgumentException(string.Format(UserDoesNotExistExceptionMessage, username));
            }

            var message = string.Empty;

            switch (property)
            {
                case "password":
                    var newValueCharArray = newValue.ToCharArray();

                    var containsLowerCase = newValue.Any(c => char.IsLower(c));
                    var containsDigit = newValue.Any(c => char.IsDigit(c));

                    if (!containsLowerCase || !containsDigit)
                    {
                        throw new ArgumentException(InvalidPasswordExceptionMessage);
                    }

                    this.users.ChangePassword(userId, newValue);
                    message = SuccessChangedPasswordMessage;
                    break;

                case "borntown":
                    var borntown = this.towns.ByName<Town>(newValue);

                    if (borntown == null)
                    {
                        throw new ArgumentException(string.Format(ValueNotValidForThatPropertyExceptionMessage, newValue, string.Format(TownNotFoundExceptionMessage, newValue)));
                    }

                    this.users.SetBornTown(userId, borntown.Id);
                    message = string.Format(SuccessModifyUserMessage, username, property, newValue);
                    break;

                case "currenttown":
                    var currentTown = this.towns.ByName<Town>(newValue);

                    if (currentTown == null)
                    {
                        throw new ArgumentException(string.Format(ValueNotValidForThatPropertyExceptionMessage, currentTown.Name, string.Format(TownNotFoundExceptionMessage, currentTown.Name)));
                    }

                    this.users.SetCurrentTown(userId, currentTown.Id);
                    message = string.Format(SuccessModifyUserMessage, username, property, newValue);
                    break;

                default:
                    throw new ArgumentException(string.Format(PropertyNotFoundExceptionMessage, property));
            }

            return message;
        }
    }
}