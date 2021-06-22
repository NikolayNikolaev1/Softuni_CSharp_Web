namespace CarDealer.Services.Models.Car
{
    using Part;
    using System.Collections.Generic;

    public class CarPartsModel
    {
        public CarModel Car { get; set; }

        public ICollection<PartModel> Parts { get; set; }
    }
}
