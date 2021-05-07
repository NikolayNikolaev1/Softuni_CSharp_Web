namespace FootballBetting.Data.Configurations
{
    using FootballBetting.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> town)
        {
            town
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

            town
                .HasMany(tn => tn.Teams)
                .WithOne(tm => tm.Town)
                .HasForeignKey(tm => tm.TownId);
        }
    }
}
