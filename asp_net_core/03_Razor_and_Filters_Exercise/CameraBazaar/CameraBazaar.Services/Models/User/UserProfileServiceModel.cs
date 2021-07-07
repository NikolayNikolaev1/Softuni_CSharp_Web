namespace CameraBazaar.Services.Models.User
{
    using System;

    public class UserProfileServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CamerasInStockCount { get; set; }

        public int CamerasOutOfStockCount { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
