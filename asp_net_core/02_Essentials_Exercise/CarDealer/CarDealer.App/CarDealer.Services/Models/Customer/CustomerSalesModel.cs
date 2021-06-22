using CarDealer.Services.Models.Car;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Services.Models.Customer
{
    public class CustomerSalesModel
    {
        public string Name { get; set; }


        public bool IsYoungDriver { get; set; }

        public ICollection<CarPriceModel> BoughtCars { get; set; }

        public decimal MoneySpent
            => this.BoughtCars
            .Sum(c => c.Price * (1 - c.Discount))
            * (this.IsYoungDriver ? 0.95m : 1);

    }
}
