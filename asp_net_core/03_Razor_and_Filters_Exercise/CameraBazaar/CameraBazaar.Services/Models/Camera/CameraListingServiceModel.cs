namespace CameraBazaar.Services.Models.Camera
{
    using Data.Enums;

    public class CameraListingServiceModel
    {
        public int Id { get; set; }

        public MakeType Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsInStock { get; set; }
    }
}
