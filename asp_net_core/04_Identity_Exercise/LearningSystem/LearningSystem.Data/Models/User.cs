namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants.Properties;

    public class User : IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        [Required]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public ICollection<Course> RegisteredCourses { get; set; } = new List<Course>();

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
