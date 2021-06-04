namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public IEnumerable<Product> Search(string searchTerm)
            => this.context
            .Products
            .Where(p => p.Name.Contains(searchTerm))
            .ToList();
    }
}
