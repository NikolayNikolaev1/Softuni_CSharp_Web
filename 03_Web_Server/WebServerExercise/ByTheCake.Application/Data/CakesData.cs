namespace ByTheCake.Application.Data
{
    using Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        private const string DatabasePath = @"..\..\..\Data\database.csv";

        public IEnumerable<Cake> All()
               => File
               .ReadAllLines(DatabasePath)
               .Where(l => l.Contains(','))
               .Select(l => l.Split(','))
               .Select(l => new Cake
               {
                   Id = int.Parse(l[0]),
                   Name = l[1],
                   Price = decimal.Parse(l[2])
               });

        public Cake Find(int id)
            => this.All().FirstOrDefault(c => c.Id == id);
    }
}
