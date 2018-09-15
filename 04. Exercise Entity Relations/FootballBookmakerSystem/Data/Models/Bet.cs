namespace FootballBookmakerSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [Range(0, 2)]
        public int Prediction { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public DateTime Date { get; set; }
    }
}