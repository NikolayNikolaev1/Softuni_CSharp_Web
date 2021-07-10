namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<CourseListingServiceModel> All()
            => this.dbContext
            .Courses
            .ProjectTo<CourseListingServiceModel>()
            .ToList();

        public void Create(
            string name,
            string description,
            string trainerId,
            DateTime startDate,
            DateTime endDate)
        {
            this.dbContext
                .Courses
                .Add(new Course
                {
                    Name = name,
                    Description = description,
                    TrainerId = trainerId,
                    StartDate = startDate,
                    EndDate = endDate
                });

            this.dbContext.SaveChanges();
        }

        public CourseDetailsServiceModel Details(int id)
        {
            var course = this.dbContext
                .Courses
                .Where(c => c.Id == id);

            if (course == null)
            {
                return null;
            }

            return course
                .ProjectTo<CourseDetailsServiceModel>()
                .FirstOrDefault();
        }

        public void Join(int courseId, string userId)
        {
            var user = this.dbContext
                .Users
                .Find(userId);

            this.dbContext
                .Courses
                .Find(courseId)
                .Students
                .Add(new StudentCourse
                { 
                    StudentId = userId
                });

            this.dbContext.SaveChanges();
        }
    }
}
