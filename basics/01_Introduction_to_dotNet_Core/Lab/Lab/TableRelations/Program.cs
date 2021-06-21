using System;
using TableRelations.Models;

namespace TableRelations
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new AppDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine("DB CREATED!");

            Department department = new Department { Name = "Test" };
            department.Employees.Add(new Employee { Name = "Pesho" });

            db.Departments.Add(department);
            db.SaveChanges();
        }
    }
}
