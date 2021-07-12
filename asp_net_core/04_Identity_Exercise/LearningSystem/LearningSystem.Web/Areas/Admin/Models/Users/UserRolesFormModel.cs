namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;

    public class UserRolesFormModel
    {
        public string UserId { get; set; }

        public ICollection<string> RoleIds { get; set; }
    }
}
