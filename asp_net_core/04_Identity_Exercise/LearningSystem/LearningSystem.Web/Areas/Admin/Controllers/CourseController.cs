namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.GlobalConstants.Roles;

    [Area("Admin")]
    [Authorize(Roles = Administrator)]
    public class CourseController : Controller
    {
        public IActionResult Create() => View();
    }
}
