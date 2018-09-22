namespace PhotoShare.App.Core.Commands
{
    using Interfaces;
    using PhotoShare.App.Infrastructure;
    using PhotoShare.Models;
    using Services;

    using static Common.SuccessMessages;

    // This command does not work correctly because there are conflicts between
    // the given skeleton for this project and the problem descriptions
    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService users;

        public AcceptFriendCommand(IUserService users)
        {
            this.users = users;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[1];
            var friendName = data[2];

            var user = this.users.ByUsername<User>(username);
            var friend = this.users.ByUsername<User>(friendName);

            var friendship = this.users.GetFriendship(user.Id, friend.Id);

            // Any of the users do not exist.
            Validator.ThrowExceptionIfUserNotFound(user);
            Validator.ThrowExceptionIfUserNotFound(friend);

            // They are already friends
            Validator.ThrowExceptionIfTheyAreAlreadyFriends(friendship, user, friend);

            // There is no such friend request.
            Validator.ThrowExceptionIfFriendshipIsNull(friendship, friendName, username);

            this.users.AcceptFriend(user.Id, friend.Id);
            return string.Format(SuccessAcceptFriendMessage, friendName, username);
        }
    }
}