namespace FootballBookmakerSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infastructure.GlobalConstants;

    public class Color
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        public List<Team> PrimaryKitTeams { get; set; } = new List<Team>();

        public List<Team> SecondaryKitTeams { get; set; } = new List<Team>();
    }
}