namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    using static Common.GlobalConstants.Roles;

    [Area("Admin")]
    [Authorize(Roles = Administrator)]
    public class CourseController : Controller
    {
        private readonly ICourseService courses;

        public CourseController(ICourseService courses)
        {
            this.courses = courses;
        }

        public IActionResult Create() => View();
    }
}
