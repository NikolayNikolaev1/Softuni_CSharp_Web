namespace CarDealer.Services.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        ICollection<CustomerListingModel> OrderedCustomers(OrderType order);
    }
}
