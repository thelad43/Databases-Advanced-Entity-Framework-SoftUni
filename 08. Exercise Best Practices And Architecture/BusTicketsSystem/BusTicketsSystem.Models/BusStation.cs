namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BusStation
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public List<Trip> DeparturesTrips { get; set; } = new List<Trip>();

        public List<Trip> ArrivingTrips { get; set; } = new List<Trip>();
    }
}