namespace LearningSystem.Services.Models.Courses
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System;

    public class CourseListingServiceModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
