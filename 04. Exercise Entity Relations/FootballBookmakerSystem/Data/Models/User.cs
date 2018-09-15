namespace FootballBookmakerSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Infastructure.GlobalConstants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Username { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}