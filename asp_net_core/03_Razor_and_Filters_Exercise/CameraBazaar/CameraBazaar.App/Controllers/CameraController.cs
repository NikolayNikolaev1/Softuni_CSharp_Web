namespace CameraBazaar.App.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Camera;
    using Services;
    using Services.Models.Camera;
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isDeleted = this.cameras.Delete(id);

            if (!isDeleted)
            {
                return this.NotFound();
            }

            return this.Redirect($"/user/profile/{this.userManager.GetUserId(User)}");
        }

        public IActionResult Details(int id)
        {
            CameraDetailsServiceModel cameraModel =  this.cameras.Details(id);

            if (cameraModel == null)
            {
                return this.NotFound();
            }

            return this.View(cameraModel);
        }

        public IActionResult Edit(int id)
        {
            CameraFormServiceModel cameraModel = this.cameras.Find(id);

            if (cameraModel == null)
            {
                return this.NotFound();
            }

            return this.View(cameraModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CameraFormServiceModel formModel)
        {
            bool isEdited = this.cameras
                .Edit(
                formModel.Id,
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
                formModel.ImageUrl);

            if (!isEdited)
            {
                return this.NotFound();
            }

            return this.Redirect($"/user/profile/{this.userManager.GetUserId(User)}");
        }
    }
}
