namespace PhotoShare.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using PhotoShare.Models;
    using Services;
    using System.Collections.Generic;
    using System.Text;

    using static Common.SuccessMessages;

    public class PrintFriendsListCommand : ICommand
    {
        private readonly IUserService users;

        public PrintFriendsListCommand(IUserService users)
        {
            this.users = users;
        }

        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            var username = data[1];
            var user = this.users.ByUsername<User>(username);

            Validator.ThrowExceptionIfUserNotFound(user);

            var friendships = user.FriendsAdded;

            var friends = new List<string>();

            foreach (var friendship in friendships)
            {
                var currentFriend = this.users.ById<User>(friendship.FriendId);

                if (friendship.UserId == user.Id && friendship.FriendId == currentFriend.Id)
                {
                    friends.Add(currentFriend.Username);
                }
            }

            if (friends.Count == 0)
            {
                return UserDoesNotHaveAnyFriendsMessage;
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Friends:");

            foreach (var friend in friends)
            {
                stringBuilder.AppendLine($"-{friend}");
            }

            return stringBuilder.ToString();
        }
    }
}