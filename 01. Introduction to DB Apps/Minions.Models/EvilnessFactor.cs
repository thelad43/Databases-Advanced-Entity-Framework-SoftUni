namespace Minions.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EvilnessFactor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Villain> Villains { get; set; } = new List<Villain>();
    }
}