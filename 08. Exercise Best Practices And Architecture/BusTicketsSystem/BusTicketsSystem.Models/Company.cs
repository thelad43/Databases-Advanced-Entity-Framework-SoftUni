namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Nationality { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Trip> Trips { get; set; } = new List<Trip>();
    }
}