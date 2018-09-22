namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using Services;
    using System;

    using static Common.ExceptionMessages;
    using static Common.SuccessMessages;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService towns;
        private readonly ISessionService session;

        public AddTownCommand(ITownService towns, ISessionService session)
        {
            this.towns = towns;
            this.session = session;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var townName = data[1];
            var country = data[2];

            var townExists = this.towns.Exists(townName);

            if (townExists)
            {
                throw new ArgumentException(string.Format(TownExistsExceptionMessage, townName));
            }

            var town = this.towns.Add(townName, country);

            return string.Format(SuccessAddTownMessage, townName);
        }
    }
}