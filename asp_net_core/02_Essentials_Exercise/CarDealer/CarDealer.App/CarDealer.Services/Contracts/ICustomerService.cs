namespace CarDealer.Services.Contracts
{
    using Models.Enums;
    using Models.Customer;
    using System.Collections.Generic;
    using System;

    public interface ICustomerService
    {
        void Create(string name, DateTime birthDate);

        CustomerModel Find(int id);

        CustomerSalesModel FindSales(int id);

        ICollection<CustomerModel> OrderedCustomers(OrderType order);
    }
}
