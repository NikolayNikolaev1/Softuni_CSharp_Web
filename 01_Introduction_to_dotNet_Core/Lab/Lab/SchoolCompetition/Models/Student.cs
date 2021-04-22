using System.Collections.Generic;
using System.Linq;

namespace SchoolCompetition.Models
{
    public class Student
    {
        private string name;
        private Dictionary<string, int> categoryPoints;

        public Student(string name)
        {
            this.Name = name;
            this.CategoryPoints = new Dictionary<string, int>();
        }

        public string Name 
        {
            get { return this.name; }
            private set { this.name = value; } 
        }

        public Dictionary<string, int> CategoryPoints { 
            get { return this.categoryPoints; } 
            private set { this.categoryPoints = value; }
        }

        public void AddCategoryPoints(string categoryName, int categoryPoints)
        {
            if (!this.CategoryPoints.ContainsKey(categoryName))
            {
                this.CategoryPoints.Add(categoryName, categoryPoints);
            }
            else
            {
                this.CategoryPoints[categoryName] += categoryPoints;
            }
        }

        public override string ToString()
        {
            return string.Format($"{this.Name}: {this.CategoryPoints.Values.Sum()} [{string.Join(", ", this.CategoryPoints.Keys)}]");
        }

    }
}
