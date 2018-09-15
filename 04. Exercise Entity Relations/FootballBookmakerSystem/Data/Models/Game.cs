namespace FootballBookmakerSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public int Id { get; set; }

        public decimal AwayTeamBetRate { get; set; }

        public decimal HomeTeamBetRate { get; set; }

        public decimal DrawTeamBetRate { get; set; }

        public int AwayTeamGoals { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        [Range(0, 2)]
        public int Result { get; set; }

        public DateTime Date { get; set; }

        public List<PlayerStatistic> Players { get; set; } = new List<PlayerStatistic>();

        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}