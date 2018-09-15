namespace FootballBookmakerSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infastructure.GlobalConstants;

    public class Position
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}