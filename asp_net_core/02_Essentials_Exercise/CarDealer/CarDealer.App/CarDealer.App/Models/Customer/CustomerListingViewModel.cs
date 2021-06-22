namespace CarDealer.App.Models.Customer
{
    using Services.Models.Enums;
    using Services.Models.Customer;
    using System.Collections.Generic;

    public class CustomerListingViewModel
    {
        public ICollection<CustomerModel> Customers { get; set; }

        public OrderType OrderType { get; set; }
    }
}
