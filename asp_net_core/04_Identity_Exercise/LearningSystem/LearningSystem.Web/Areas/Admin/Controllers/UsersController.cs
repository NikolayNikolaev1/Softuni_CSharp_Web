namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    using static Common.GlobalConstants.Roles;

    [Area("Admin")]
    [Authorize(Roles = Administrator)]
    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        public IActionResult All()
        {
            return View(this.users.All());
        }
    }
}
