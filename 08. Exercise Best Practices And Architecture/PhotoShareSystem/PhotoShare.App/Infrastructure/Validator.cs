namespace PhotoShare.App.Infrastructure
{
    using Models;
    using Services;
    using System;

    using static Common.ExceptionMessages;

    public static class Validator
    {
        public static void ThrowExceptionIfUserNotFound(User user)
        {
            if (user == null)
            {
                throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, user.Username));
            }
        }

        public static void ThrowExceptionIfUserIsDeleted(User user, string username)
        {
            if (user.IsDeleted.Value)
            {
                throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username));
            }
        }

        public static void ThrowExceptionIfFriendshipIsNull(Friendship friendship, string friendName, string username)
        {
            if (friendship == null)
            {
                throw new InvalidOperationException(string.Format(ThereIsNoSuchFriendRequestExceptionMessage, friendName, username));
            }
        }

        public static void ThrowExceptionIfFriendshipIsNotNull(Friendship friendship, string friendName, string username)
        {
            if (friendship != null)
            {
                throw new InvalidOperationException(string.Format(TheyAreAlreadyFriendsExceptionMessage, friendName, username));
            }
        }

        public static void ThrowExceptionIfTheyAreAlreadyFriends(Friendship friendship, User user, User friend)
        {
            if (user.FriendsAdded.Contains(friendship) && friend.FriendsAdded.Contains(friendship))
            {
                throw new InvalidOperationException(string.Format(TheyAreAlreadyFriendsExceptionMessage, friend.FirstName, user.FirstName));
            }
        }

        public static void ThrowExceptionIfUserIsLoggedIn(ISessionService session)
        {
            if (session.IsLoggedIn())
            {
                throw new ArgumentException(InvalidCredentialsExceptionMessage);
            }
        }

        public static void ThrowExceptionIfUserIsNotLoggedIn(ISessionService session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException(InvalidCredentialsExceptionMessage);
            }
        }
    }
}