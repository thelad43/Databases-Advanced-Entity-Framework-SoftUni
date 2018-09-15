namespace FootballBookmakerSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infastructure.GlobalConstants;

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        public decimal Budget { get; set; }

        public string Initials { get; set; }

        public string LogoUrl { get; set; }

        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public List<Game> HomeGames { get; set; } = new List<Game>();

        public List<Game> AwayGames { get; set; } = new List<Game>();
    }
}