namespace PhotoShare.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public List<User> UsersBornInTown { get; set; } = new List<User>();

        public List<User> UsersCurrentlyLivingInTown { get; set; } = new List<User>();
    }
}