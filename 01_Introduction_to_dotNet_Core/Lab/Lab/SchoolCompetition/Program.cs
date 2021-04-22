using SchoolCompetition.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolCompetition
{
    class Program
    {
        private const string END_COMMAND = "END";
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            string input;

            while (!(input = Console.ReadLine()).Equals(END_COMMAND))
            {
                string[] inputArgs = input.Split();
                string studentName = inputArgs[0];
                string categoryName = inputArgs[1];
                int categoryPoints = int.Parse(inputArgs[2]);

                var currentStudent = students.Where(s => s.Name.Equals(studentName));

                if (currentStudent.Any())
                {
                    currentStudent.First()
                        .AddCategoryPoints(categoryName, categoryPoints);
                    continue;
                }

                Student student = new Student(studentName);
                student.AddCategoryPoints(categoryName, categoryPoints);
                students.Add(student);
            }

            students
                .OrderByDescending(s => s.CategoryPoints.Values.Sum())
                .ThenBy(s => s.Name)
                .ToList()
                .ForEach(s => Console.WriteLine(s.ToString()));
        }
    }
}
