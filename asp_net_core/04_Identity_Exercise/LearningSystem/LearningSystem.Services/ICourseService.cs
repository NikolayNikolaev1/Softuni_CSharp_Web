namespace LearningSystem.Services
{
    using Models.Courses;
    using System;
    using System.Collections.Generic;

    public interface ICourseService
    {
        ICollection<CourseListingServiceModel> All();

        void Create(
            string name,
            string description,
            string trainerId,
            DateTime startDate,
            DateTime endDate);

        CourseDetailsServiceModel Details(int id);

        void Join(int courseId, string userId);
    }
}
