namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class CourseController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public CourseController(ICourseService courses, UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var serviceModel = this.courses.Details(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(serviceModel);
        }

        [Authorize]
        public IActionResult Join(int id)
        {
            this.courses.Join(id, this.userManager.GetUserId(User));

            return RedirectToAction("Index", "Home");
        }
    }
}
