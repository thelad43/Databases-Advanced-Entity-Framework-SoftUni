namespace Minions.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Minion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public List<MinionsVillains> Villains { get; set; } = new List<MinionsVillains>();
    }
}