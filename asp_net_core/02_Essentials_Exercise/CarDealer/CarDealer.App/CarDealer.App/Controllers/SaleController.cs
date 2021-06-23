namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class SaleController : Controller
    {
        private readonly ISaleService sales;

        public SaleController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("/sales")]
        public IActionResult All()
            => View(this.sales.All());

        [Route("/sales/{id}", Order = 2)]
        public IActionResult Details(int id)
            => View(this.sales.Find(id));

        [Route("/sales/discounted", Order = 1)]
        public IActionResult Discounted()
            => View(this.sales.AllDiscounted());

        [Route("/sales/discounted/{percent}")]
        public IActionResult DiscountedPercent(decimal percent)
            => View(this.sales.AllDiscounted(percent));
    }
}
