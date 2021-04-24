using FootballBetting.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data
{
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HP-ELITEBOOK\SQLEXPRESS;Database=FootballBettingDatabase;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.PrimaryKitColorId);

            modelBuilder
                .Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.SecondaryKitColorId);

            modelBuilder
                .Entity<Team>()
                .HasOne(tm => tm.Town)
                .WithMany(tn => tn.Teams)
                .HasForeignKey(tm => tm.TownId);

            modelBuilder
                .Entity<Town>()
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

            modelBuilder
                .Entity<CountryContinent>()
                .HasKey(cc => new { cc.CountryId, cc.ContinentId });

            modelBuilder
                .Entity<Country>()
                .HasMany(c => c.Continents)
                .WithOne(cc => cc.Country)
                .HasForeignKey(cc => cc.CountryId);

            modelBuilder
                .Entity<Continent>()
                .HasMany(c => c.Countries)
                .WithOne(cc => cc.Continent)
                .HasForeignKey(cc => cc.ContinentId);

            modelBuilder
                .Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder
                .Entity<Player>()
                .HasOne(pl => pl.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(pl => pl.PositionId);

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.PlayerId, ps.GameId });

            modelBuilder
                .Entity<Player>()
                .HasMany(p => p.PlayerStatistics)
                .WithOne(ps => ps.Player)
                .HasForeignKey(ps => ps.PlayerId);

            modelBuilder
                .Entity<Game>()
                .HasMany(g => g.PlayerStatistics)
                .WithOne(ps => ps.Game)
                .HasForeignKey(ps => ps.GameId);

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);

            modelBuilder
                .Entity<GameBet>()
                .HasKey(gb => new { gb.GameId, gb.BetId });

            modelBuilder
                .Entity<Game>()
                .HasMany(g => g.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(b => b.GameId);

            modelBuilder
                .Entity<Bet>()
                .HasMany(b => b.Games)
                .WithOne(g => g.Bet)
                .HasForeignKey(g => g.BetId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
