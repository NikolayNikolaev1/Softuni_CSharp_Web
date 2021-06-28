namespace CarDealer.App.Controllers
{
    using CarDealer.Services.Models.Car;
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

        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            this.cars.Add(carModel.Make, carModel.Model, carModel.TraveledDistance);

            return Redirect("/");
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
