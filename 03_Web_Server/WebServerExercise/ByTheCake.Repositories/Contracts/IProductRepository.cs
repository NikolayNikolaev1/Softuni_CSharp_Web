namespace ByTheCake.Providers.Contracts
{
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;

    public interface IProductRepository : IRepository<Product>
    {
        Product Create(string name, decimal price, string imageUrl);

        CakeDetailsViewMovdel Details(int id);

        IEnumerable<ProductListingViewModel> Search(string searchTerm = null);
    }
}
