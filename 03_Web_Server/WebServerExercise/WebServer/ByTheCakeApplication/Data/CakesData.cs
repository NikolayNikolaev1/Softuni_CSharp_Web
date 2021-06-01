namespace WebServer.ByTheCakeApplication.Data
{
    using Infrastructure;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        public IEnumerable<Cake> All()
            => File
            .ReadAllLines(Controller.DatabasePath)
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
