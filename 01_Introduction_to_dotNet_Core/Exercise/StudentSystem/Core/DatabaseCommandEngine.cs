namespace StudentSystem.Core
{
    using StudentSystem.Database;
    using StudentSystem.Enums;
    using StudentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DatabaseCommandEngine
    {
        private AppDbContext dbContext;

        public DatabaseCommandEngine(AppDbContext db)
        {
            this.DbContext = db;
        }

        public AppDbContext DbContext
        {
            get { return this.dbContext; }
            private set { this.dbContext = value; }
        }

        public void InsertDefaultData()
        // hardcode used only for testing   
        {
            var currentDate = DateTime.Now;
            const int totalStudents = 25;
            const int totalCourses = 10;

            for (int i = 0; i < totalStudents; i++)
            {
                this.DbContext.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(1),
                    BirthDay = currentDate.AddYears(-20).AddDays(1),
                    PhoneNumber = $"Random Phone {i}"
                });

            }

            this.DbContext.SaveChanges();

            var addedCourses = new List<Course>();

            for (int i = 0; i < totalStudents; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course details {i}",
                    Price = 100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(i + 20)
                }; 

                addedCourses.Add(course);
                this.DbContext.Add(course);
            }

            this.dbContext.SaveChanges();

            var random = new Random();

            var studentIds = this.DbContext
                .Students
                .Select(s => s.Id)
                .ToList();

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCourse = random.Next(2, totalStudents / 2);

                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];

                    if (!currentCourse.Studens.Any(s => s.StudentId == studentId))
                    {
                        currentCourse.Studens.Add(new StudentCourse
                        {
                            StudentId = studentId
                        });
                    }
                    else
                    {
                        j--;
                    }

                }

                var resourcesInCourse = random.Next(2, 20);
                var types = new[] { 0, 1, 2, 999 };

                currentCourse.Resources.Add(new Resource
                {
                    Name = $"Resource {i}",
                    URL = $"Url {i}",
                    TypeOfResource = (ResourceType)types[random.Next(0, types.Length)]
                });
            }

            this.DbContext.SaveChanges();

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];

                var studentInCourseIds = currentCourse
                    .Studens
                    .Select(s => s.StudentId)
                    .ToList();

                for (int j = 0; j < studentInCourseIds.Count; j++)
                {
                    var totalHomeworks = random.Next(2, 5);

                    for (int k = 0; k < totalHomeworks; k++)
                    {
                        dbContext.Homeworks.Add(new Homework
                        {
                            Content = $"Content Homework {i}",
                            SubmitionDate = currentDate.AddDays(-i),
                            ContentType = ContentType.Zip,
                            StudentId = studentInCourseIds[j],
                            CourseId = currentCourse.Id
                        });
                    }
                }

                this.dbContext.SaveChanges();
            }

        }
    }
}
