namespace CameraBazaar.Services.Models.User
{
    using Camera;
    using System.Collections.Generic;

    public class UserProfileServiceModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CamerasInStockCount { get; set; }

        public int CamerasOutOfStockCount { get; set; }

        public ICollection<CameraListingServiceModel> CameraList { get; set; }
    }
}
