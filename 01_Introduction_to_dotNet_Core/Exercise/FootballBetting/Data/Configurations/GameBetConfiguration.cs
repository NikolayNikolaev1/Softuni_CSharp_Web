namespace FootballBetting.Data.Configurations
{
    using FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GameBetConfiguration : IEntityTypeConfiguration<GameBet>
    {
        public void Configure(EntityTypeBuilder<GameBet> gameBets)
        {
            gameBets
                .HasKey(gb => new { gb.GameId, gb.BetId });

            gameBets
                .HasOne(gb => gb.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(gb => gb.GameId);

            gameBets
                .HasOne(gb => gb.Bet)
                .WithMany(b => b.Games)
                .HasForeignKey(gb => gb.BetId);

            gameBets
                .HasOne(gb => gb.ResultPrediction)
                .WithMany(rp => rp.GameBets)
                .HasForeignKey(gb => gb.ResultPredictionId);
        }
    }
}
