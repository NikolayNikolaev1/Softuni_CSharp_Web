namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Car;
    using Services.Contracts;

    public class CarController : Controller
    {
        private readonly ICarService cars;

        public CarController(ICarService cars)
        {
            this.cars = cars;
        }

        [Route("/cars/{make}")]
        public IActionResult All(string make)
            => View(new CarListingViewModel
            {
                Cars = this.cars.GetByMake(make),
                Make = make
            });

        [Route("/cars/{id}/parts")]
        public IActionResult PartsList(int id)
            => View(this.cars.FindWithParts(id));
    }
}
