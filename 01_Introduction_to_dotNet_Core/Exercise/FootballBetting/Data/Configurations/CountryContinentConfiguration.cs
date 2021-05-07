namespace FootballBetting.Data.Configurations
{
    using FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountryContinentConfiguration : IEntityTypeConfiguration<CountryContinent>
    {
        public void Configure(EntityTypeBuilder<CountryContinent> countryContinent)
        {
            countryContinent
                .HasKey(cc => new { cc.CountryId, cc.ContinentId });

            countryContinent
                .HasOne(cc => cc.Country)
                .WithMany(c => c.Continents)
                .HasForeignKey(cc => cc.CountryId);

            countryContinent
                .HasOne(cc => cc.Continent)
                .WithMany(c => c.Countries)
                .HasForeignKey(cc => cc.ContinentId);
        }
    }
}
