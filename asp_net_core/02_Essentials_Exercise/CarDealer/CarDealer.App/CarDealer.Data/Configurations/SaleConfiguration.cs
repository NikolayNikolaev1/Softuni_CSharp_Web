namespace CarDealer.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> sale)
        {
            sale
                .HasOne(s => s.Car)
                .WithOne(c => c.Sale)
                .HasForeignKey<Sale>(s => s.CarId)
                .IsRequired();

            sale
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .IsRequired();
        }
    }
}
