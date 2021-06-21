namespace FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        [StringLength(3)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CountryContinent> Continents { get; set; } = new List<CountryContinent>();

        public List<Town> Towns { get; set; } = new List<Town>();
    }
}
