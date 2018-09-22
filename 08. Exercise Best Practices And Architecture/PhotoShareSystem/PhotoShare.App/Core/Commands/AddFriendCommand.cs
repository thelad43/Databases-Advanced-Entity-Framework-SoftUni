namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using PhotoShare.Models;
    using PhotoShare.Services;
    using System.Linq;

    using static Common.SuccessMessages;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService users;
        private readonly ISessionService session;

        public AddFriendCommand(IUserService users, ISessionService session)
        {
            this.users = users;
            this.session = session;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            Validator.ThrowExceptionIfUserIsNotLoggedIn(this.session);

            var username = data[1];
            var friendName = data[2];

            var user = this.users.ByUsername<User>(username);
            var friend = this.users.ByUsername<User>(friendName);
            var friendship = user.FriendsAdded.FirstOrDefault(f => f.Friend == friend);

            // Any of the users do not exist.
            Validator.ThrowExceptionIfUserNotFound(user);
            Validator.ThrowExceptionIfUserNotFound(friend);

            // They are already friends.
            Validator.ThrowExceptionIfFriendshipIsNotNull(friendship, friendName, username); // test it

            this.users.AddFriend(user.Id, friend.Id);
            return string.Format(SuccessAddFriendMessage, friendName, username);
        }
    }
}