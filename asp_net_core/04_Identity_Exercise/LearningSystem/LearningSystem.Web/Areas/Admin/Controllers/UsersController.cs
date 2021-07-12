namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services;
    using System.Linq;

    using static Common.WebConstants.Roles;

    [Area("Admin")]
    [Authorize(Roles = Administrator)]
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly RoleManager<Role> roleManager;

        public UsersController(IUserService users, RoleManager<Role> roleManager)
        {
            this.users = users;
            this.roleManager = roleManager;
        }

        public IActionResult All()
        {
            var serviceModel = this.users.All();
            var roles = this.roleManager.Roles.ToList();

            return View(new UserListingViewModel
            {
                Users = serviceModel,
                Roles = roles
            });
        }

        public IActionResult SetRoles(UserRolesFormModel formModel)
        {
            bool result = this.users.SetRoles(formModel.UserId, formModel.RoleIds);

            if (!result)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage("Roles changed succcessfully.");
            return RedirectToAction(nameof(All));
        }
    }
}
