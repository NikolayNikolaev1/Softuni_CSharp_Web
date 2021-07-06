namespace CameraBazaar.Services.Models.User
{
    public class UserProfileServiceModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CamerasInStockCount { get; set; }

        public int CamerasOutOfStockCount { get; set; }
    }
}
