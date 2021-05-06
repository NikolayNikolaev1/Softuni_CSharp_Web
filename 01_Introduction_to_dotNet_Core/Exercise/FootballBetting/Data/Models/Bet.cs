namespace FootballBetting.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Bet
    {
        public int Id { get; set; }

        public decimal BeyMoney { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<GameBet> Games { get; set; } = new List<GameBet>();
    }
}
