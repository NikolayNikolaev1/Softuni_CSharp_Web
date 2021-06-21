namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte Logo { get; set; }

        [StringLength(3)]
        public string Initials { get; set; }

        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public decimal Budget { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        [InverseProperty("HomeTeam")]
        public List<Game> HomeGames { get; set; } = new List<Game>();

        [InverseProperty("AwayTeam")]
        public List<Game> AwayGames { get; set; } = new List<Game>();
    }
}
