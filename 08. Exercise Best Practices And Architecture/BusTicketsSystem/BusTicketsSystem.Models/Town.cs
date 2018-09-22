namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<BusStation> BusStations { get; set; } = new List<BusStation>();
    }
}