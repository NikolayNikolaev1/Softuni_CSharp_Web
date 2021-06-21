namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [InverseProperty("PrimaryKitColor")]
        public List<Team> PrimaryColorTeams { get; set; } = new List<Team>();

        [InverseProperty("SecondaryKitColor")]
        public List<Team> SecondaryColorTeams { get; set; } = new List<Team>();
    }
}
