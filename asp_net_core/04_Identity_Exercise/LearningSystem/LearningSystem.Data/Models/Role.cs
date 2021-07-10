namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> Users { get; set; }
    }
}
