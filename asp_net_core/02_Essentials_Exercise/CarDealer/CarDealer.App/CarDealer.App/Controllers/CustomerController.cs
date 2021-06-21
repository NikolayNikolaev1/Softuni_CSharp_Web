namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Linq;

    public class CustomerController : Controller
    {
        private readonly ICustomerService customers;

        public CustomerController(ICustomerService customers)
        {
            this.customers = customers;
        }

        public IActionResult All(string order)
        {
            if (string.IsNullOrEmpty(order))
            {
                return NotFound();
            }

            if (order.Equals("ascending"))
            {
                return View(customers
                    .OrderedCustomers()
                    .OrderBy(c => c.BirthDate));
            }
            else if (order.Equals("descending"))
            {
                return View(customers
                    .OrderedCustomers()
                    .OrderByDescending(c => c.BirthDate));
            }

            return NotFound();
        }
    }
}
