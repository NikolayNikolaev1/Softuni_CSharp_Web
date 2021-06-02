namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }
    }
}
