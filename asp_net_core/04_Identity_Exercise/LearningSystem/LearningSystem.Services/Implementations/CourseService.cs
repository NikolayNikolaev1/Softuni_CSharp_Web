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

        public bool HasStudent(int courseId, string studentId)
        {
            User student = this.dbContext
                .Users
                .Find(studentId);
            Course course = this.dbContext
                .Courses
                .Find(courseId);

            if (student == null || course == null)
            {
                return false;
            }

            return this.dbContext
                .Courses
                .Any(c => c.Students
                    .Any(s => s.StudentId == studentId));
        }

        public bool SignOut(int courseId, string studentId)
        {
            User user = this.dbContext
                .Users
                .Find(studentId);
            Course course = this.dbContext
                .Courses
                .Find(courseId);

            if (user == null || course == null)
            {
                return false;
            }

            if (!this.HasStudent(courseId, studentId))
            {
                return false;
            }

            if (course.StartDate >= DateTime.UtcNow)
            {
                return false;
            }

            course.Students.Remove(this.dbContext.StudentCourses
                .FirstOrDefault(sc => sc.CourseId == courseId && sc.StudentId == studentId));
            this.dbContext.SaveChanges();
            return true;
        }

        public bool SignUp(int courseId, string studentId)
        {
            User user = this.dbContext
                .Users
                .Find(studentId);
            Course course = this.dbContext
                .Courses
                .Find(courseId);

            if (user == null || course == null)
            {
                return false;
            }

            if (this.HasStudent(courseId, studentId))
            {
                return false;
            }

            course.Students
                .Add(new StudentCourse
                {
                    StudentId = studentId
                });

            this.dbContext.SaveChanges();
            return true;
        }

    }
}
