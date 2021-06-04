namespace ByTheCake.Providers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Search(string searchTerm);
    }
}
