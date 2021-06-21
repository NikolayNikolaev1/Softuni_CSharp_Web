namespace FootballBetting.Data
{
    using FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class FootballBettingDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<GameBet> GameBets { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ResultPrediction> ResultPredictions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HP-ELITEBOOK\SQLEXPRESS;Database=FootballBettingDatabase;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
