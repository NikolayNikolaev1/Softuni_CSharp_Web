namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public Product Create(string name, decimal price, string imageUrl)
            => new Product
            {
                Name = name,
                Price = price,
                ImageUrl = imageUrl
            };

        public CakeDetailsViewMovdel Details(int id)
            => this.context
            .Products
            .Where(p => p.Id == id)
            .Select(p => new CakeDetailsViewMovdel
            {
                Name = p.Name,
                Price = p.Price,
                PictureUrl = p.ImageUrl
            }).FirstOrDefault();

        public IEnumerable<ProductListingViewModel> Search(string searchTerm = null)
        {
            IEnumerable<ProductListingViewModel> result = this.context
                .Products
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();

            if (searchTerm == null)
            {
                return result;
            }

            return result
                .Where(p => p.Name.Contains(searchTerm));
        }
    }
}
