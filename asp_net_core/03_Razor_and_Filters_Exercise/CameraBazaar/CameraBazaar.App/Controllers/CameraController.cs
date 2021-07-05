namespace CameraBazaar.App.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Camera;
    using Services;
    using System.Linq;

    public class CameraController : Controller
    {
        private readonly ICameraService cameras;
        private readonly UserManager<User> userManager;

        public CameraController(ICameraService cameras, UserManager<User> userManager)
        {
            this.cameras = cameras;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(formModel);
            }

            this.cameras.Add(
                formModel.Make,
                formModel.Model,
                formModel.Price,
                formModel.Quantity,
                formModel.MinShutterSpeed,
                formModel.MaxShutterSpeed,
                formModel.MinIso,
                formModel.MaxIso,
                formModel.IsFullFrame,
                formModel.VideoResolution,
                formModel.LightMetering.Cast<int>().Sum(),
                formModel.Description,
                formModel.ImageUrl,
                userManager.GetUserId(User));

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult All()
            => this.View(this.cameras.All());

        public IActionResult Details(int id)
        {
            var cameraModel =  this.cameras.Find(id);

            if (cameraModel == null)
            {
                return this.NotFound();
            }

            return this.View(cameraModel);
        }
    }
}
