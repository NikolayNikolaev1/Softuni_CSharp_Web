namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public DateTime? BirthDate { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public ICollection<Course> RegisteredCourses { get; set; } = new List<Course>();
    }
}
