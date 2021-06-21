namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Continent
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CountryContinent> Countries { get; set; } = new List<CountryContinent>();
    }
}
