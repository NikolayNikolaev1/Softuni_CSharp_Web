namespace CarDealer.Services.Models.Car
{
    using System.ComponentModel.DataAnnotations;

    public class CarModel
    {
        [Required(ErrorMessage = "Required field!")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public double TraveledDistance { get; set; }
    }
}
