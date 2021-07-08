﻿namespace CameraBazaar.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.User;
    using Services;

    [Authorize]
    public class UserController : Controller
    {
        private readonly ICameraService cameras;
        private readonly IUserService users;

        public UserController(ICameraService cameras, IUserService users)
        {
            this.cameras = cameras;
            this.users = users;
        }

        public IActionResult Profile(string id)
        {
            var userModel = this.users.Profile(id);

            if (userModel == null)
            {
                return this.NotFound();
            }

            var cameraListModel = this.cameras.All(id);

            return this.View(new UserProfileViewModel
            { 
                UserInfo = userModel,
                CameraList = cameraListModel
            });
        }
    }
}
