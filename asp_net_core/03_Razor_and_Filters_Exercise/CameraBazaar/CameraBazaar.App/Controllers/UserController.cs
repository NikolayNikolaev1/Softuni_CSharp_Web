namespace CameraBazaar.App.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.User;
    using Services;

    public class UserController : Controller
    {
        private readonly ICameraService cameras;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public UserController(ICameraService cameras, IUserService users, UserManager<User> userManager)
        {
            this.cameras = cameras;
            this.users = users;
            this.userManager = userManager;
        }

        public IActionResult Profile(string id)
        {
            string userId = this.userManager.GetUserId(User);

            var userModel = this.users.Profile(userId);
            var cameraListModel = this.cameras.All(userId);

            return this.View(new UserProfileViewModel
            { 
                UserInfo = userModel,
                CameraList = cameraListModel
            });
        }
    }
}
