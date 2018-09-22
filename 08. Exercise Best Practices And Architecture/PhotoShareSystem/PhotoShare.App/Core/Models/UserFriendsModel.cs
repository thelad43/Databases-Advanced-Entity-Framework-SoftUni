namespace PhotoShare.App.Core.Models
{
    using System.Collections.Generic;

    public class UserFriendsModel
    {
        public string Username { get; set; }

        public ICollection<FriendModel> Friends { get; set; }
    }
}