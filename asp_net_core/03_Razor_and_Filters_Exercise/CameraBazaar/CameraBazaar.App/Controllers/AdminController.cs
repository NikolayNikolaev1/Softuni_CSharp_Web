namespace CameraBazaar.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserService users;

        public AdminController(IUserService users)
        {
            this.users = users;
        }

        public IActionResult Allow(string id)
        {
            bool result = this.users.Allow(id);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(Users));
        }

        public IActionResult Restrict(string id)
        {

            bool result = this.users.Restrict(id);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction(nameof(Users));
        }

        public IActionResult Users()
        {
            return this.View(this.users.All());
        }

    }
}
