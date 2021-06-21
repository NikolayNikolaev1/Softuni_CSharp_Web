namespace StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Core;
    using StudentSystem.Database;
    using StudentSystem.Models;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.Migrate();

                DatabaseCommandEngine engine = new DatabaseCommandEngine(db);

                if (!db.Students.Any())
                {
                    // Add test data if there is none.
                    engine.InsertDefaultData();
                }

                //PrintStudentsAndHomework(engine.GetAllStudentsAndHomeworks());
                //PrintCoursesAndResources(engine.GetAllCoursesAndResources());
                //PrintCoursesWithMoreThanFiveResources(engine.GetAllCoursesAndResources());
                //PrintCoursesWithActiveDate(engine.GetAllCoursesWithStudents(), Convert.ToDateTime("25/04/2021"));
                //engine.PrintAllStudentsWithCourses();

            }
        }

        private static void PrintStudentsAndHomework(Student[] students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"Student {student.Name}:");

                foreach (Homework studentHomework in student.Homeworks)
                {
                    Console.WriteLine($"{studentHomework.Content}.{studentHomework.ContentType}");
                }
            }
        }

        private static void PrintCoursesAndResources(Course[] courses)
        {
            foreach (Course course in courses.OrderBy(c => c.StartDate).ThenByDescending(c => c.EndDate))
            {
                Console.WriteLine($"Crouse: {course.Name} - {course.Description}:");

                foreach (Resource courseResource in course.Resources)
                {
                    Console.WriteLine($"{courseResource.Name} - {courseResource.URL} - {courseResource.TypeOfResource}");
                }
            }
        }

        private static void PrintCoursesWithMoreThanFiveResources(Course[] courses)
        {
            Course[] filteredCourses = courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .ToArray();

            foreach (Course course in filteredCourses)
            {
                Console.WriteLine($"{course.Name} - {course.Resources.Count} courses");
            }
        }

        private static void PrintCoursesWithActiveDate(Course[] courses, DateTime activeDate)
        {

            if (!courses.Any(c => c.StartDate <= activeDate && c.EndDate >= activeDate))
            {
                Console.WriteLine("There are no courses on given date.");
            }
            else
            {
                Course currentCourse = courses.Where(c => c.StartDate <= activeDate && c.EndDate >= activeDate).First();

                Console.WriteLine($"{currentCourse.Name}: {currentCourse.StartDate}-{currentCourse.EndDate}" +
                    $"({currentCourse.EndDate - currentCourse.StartDate}) Students Count - {currentCourse.Studens.Count}");
            }
        }
    }
}
