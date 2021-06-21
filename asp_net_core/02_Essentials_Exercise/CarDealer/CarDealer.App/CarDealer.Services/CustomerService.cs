namespace CarDealer.Services
{
    using Contracts;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext dbContext;

        public CustomerService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<CustomerListingModel> OrderedCustomers(OrderType order)
        { 

        }
    }
}
