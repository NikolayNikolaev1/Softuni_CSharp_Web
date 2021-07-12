namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using Data.Models;
    using Services.Models.Users;
    using System.Collections.Generic;

    public class UserListingViewModel
    {
        public ICollection<UserListingServiceModel> Users { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
