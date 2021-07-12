namespace LearningSystem.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
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

            string userId = this.userManager.GetUserId(User);
            bool isUserInCourse;

            if (userId == null)
            {
                isUserInCourse = false;
            }
            else
            {
                isUserInCourse = this.courses
                    .HasStudent(id, userId);
            }

            return View(new CourseDetailsViewModel
            {
                CourseDetails = serviceModel,
                IsUserInCourse = isUserInCourse
            });
        }


        [Authorize]
        public IActionResult SignOut(int id)
        {
            this.courses.SignOut(id, this.userManager.GetUserId(User));
            TempData.AddSuccessMessage("Successfully signed out of course.");

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        public IActionResult SignUp(int id)
        {
            this.courses.SignUp(id, this.userManager.GetUserId(User));
            TempData.AddSuccessMessage("Successfully signed in course.");

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
