namespace LearningSystem.Services.Models.Courses
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System;

    public class CourseDetailsServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public User Trainer { get; set; }
    }
}
