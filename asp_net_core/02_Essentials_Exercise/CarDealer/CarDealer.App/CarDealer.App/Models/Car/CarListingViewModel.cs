namespace CarDealer.App.Models.Car
{
    using Services.Models.Car;
    using System.Collections.Generic;

    public class CarListingViewModel
    {
        public ICollection<CarModel> Cars { get; set; }

        public string Make { get; set; }
    }
}
