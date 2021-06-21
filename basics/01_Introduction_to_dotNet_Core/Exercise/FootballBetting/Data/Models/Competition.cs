namespace FootballBetting.Data.Models
{
    using FootballBetting.Data.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Competition
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public List<Game> Games { get; set; }
    }
}
