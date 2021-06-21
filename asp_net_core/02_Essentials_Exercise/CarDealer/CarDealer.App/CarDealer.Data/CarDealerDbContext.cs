namespace CarDealer.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;

    public class CarDealerDbContext : DbContext
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
