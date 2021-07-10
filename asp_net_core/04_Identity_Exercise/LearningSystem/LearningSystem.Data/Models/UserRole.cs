namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
