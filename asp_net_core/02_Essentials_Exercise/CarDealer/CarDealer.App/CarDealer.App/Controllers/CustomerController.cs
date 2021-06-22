namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Customer;
    using Services.Contracts;
    using Services.Models.Enums;
    using Services.Models.Customer;
    using System.Collections.Generic;

    public class CustomerController : Controller
    {
        private readonly ICustomerService customers;

        public CustomerController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("/customers/all/{order}")]
        public IActionResult All(string order)
        {
            if (string.IsNullOrEmpty(order))
            {
                return NotFound();
            }

            OrderType orderType = order.ToLower() == "descending"
                ? OrderType.Descending
                : OrderType.Ascending;

            ICollection<CustomerModel> customers = this.customers.OrderedCustomers(orderType);

            return View(new CustomerListingViewModel
            {
                Customers = customers,
                OrderType = orderType
            });
        }

        [Route("/customers/{id}")]
        public IActionResult GetSales(int id)
            => View(this.customers.FindSales(id));
    }
}
