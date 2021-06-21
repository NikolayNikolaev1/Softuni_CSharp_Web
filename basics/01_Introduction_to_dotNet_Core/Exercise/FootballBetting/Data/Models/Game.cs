namespace FootballBetting.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public string HomeGoals { get; set; }

        public string AwayGoals { get; set; }

        public DateTime DateTime { get; set; }

        public int HomeTeamWinBetRate { get; set; }

        public int AwayTeamWinBetRate { get; set; }

        public int DrawBetRate { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public List<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();

        public List<GameBet> Bets { get; set; } = new List<GameBet>();
    }
}
