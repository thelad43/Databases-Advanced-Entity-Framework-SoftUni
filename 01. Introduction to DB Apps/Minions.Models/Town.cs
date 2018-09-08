namespace Minions.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<Minion> Minions { get; set; } = new List<Minion>();
    }
}