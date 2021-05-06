﻿namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Team> PrimaryColorTeams { get; set; } = new List<Team>();

        public List<Team> SecondaryColorTeams { get; set; } = new List<Team>();
    }
}
