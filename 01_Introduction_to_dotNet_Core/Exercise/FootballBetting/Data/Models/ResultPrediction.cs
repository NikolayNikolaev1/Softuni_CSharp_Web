namespace FootballBetting.Data.Models
{
    using FootballBetting.Data.Enums;
    using System.Collections.Generic;

    public class ResultPrediction
    {
        public int Id { get; set; }

        public Prediction Prediction { get; set; }

        public List<GameBet> GameBets { get; set; } = new List<GameBet>();
    }
}
