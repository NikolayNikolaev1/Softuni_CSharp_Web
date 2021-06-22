namespace CarDealer.Services.Contracts
{
    using Models.Enums;
    using Models.Customer;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        CustomerSalesModel FindSales(int id);

        ICollection<CustomerModel> OrderedCustomers(OrderType order);
    }
}
