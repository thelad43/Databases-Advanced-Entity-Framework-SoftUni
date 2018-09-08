namespace Minions.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Villain
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int EvilnessFactorId { get; set; }

        public EvilnessFactor EvilnessFactor { get; set; }

        public List<MinionsVillains> Minions { get; set; } = new List<MinionsVillains>();
    }
}