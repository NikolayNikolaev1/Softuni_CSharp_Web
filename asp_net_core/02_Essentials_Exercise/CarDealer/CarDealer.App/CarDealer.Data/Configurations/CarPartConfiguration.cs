namespace CarDealer.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarPartConfiguration : IEntityTypeConfiguration<CarPart>
    {
        public void Configure(EntityTypeBuilder<CarPart> carPart)
        {
            carPart
                .HasKey(cp => new { cp.CarId, cp.PartId });

            carPart
                .HasOne(cp => cp.Car)
                .WithMany(c => c.Parts)
                .HasForeignKey(cp => cp.CarId);

            carPart
                .HasOne(cp => cp.Part)
                .WithMany(p => p.Cars)
                .HasForeignKey(cp => cp.PartId);
        }
    }
}
