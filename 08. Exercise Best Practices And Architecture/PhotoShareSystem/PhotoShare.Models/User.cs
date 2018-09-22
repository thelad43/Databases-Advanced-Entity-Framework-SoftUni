namespace PhotoShare.Models
{
    using Common;
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [Password(6, 20)]
        public string Password { get; set; }

        [Email]
        public string Email { get; set; }

        public int? ProfilePictureId { get; set; }

        public Picture ProfilePicture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public int? BornTownId { get; set; }

        public Town BornTown { get; set; }

        public int? CurrentTownId { get; set; }

        public Town CurrentTown { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Age]
        public int? Age { get; set; }

        public bool? IsDeleted { get; set; }

        public List<Friendship> FriendsAdded { get; set; } = new List<Friendship>();

        public List<AlbumRole> AlbumRoles { get; set; } = new List<AlbumRole>();
    }
}