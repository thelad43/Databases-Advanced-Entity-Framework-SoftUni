namespace FootballBookmakerSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infastructure.GlobalConstants;

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        public List<Town> Towns { get; set; } = new List<Town>();
    }
}