namespace CameraBazaar.App.Models.User
{
    using Services.Models.Camera;
    using Services.Models.User;
    using System.Collections.Generic;

    public class UserProfileViewModel
    {
        public UserProfileServiceModel UserInfo { get; set; }

        public ICollection<CameraListingServiceModel> CameraList { get; set; }
    }
}
