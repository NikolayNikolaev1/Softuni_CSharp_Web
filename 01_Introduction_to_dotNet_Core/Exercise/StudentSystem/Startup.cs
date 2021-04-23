namespace StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Core;
    using StudentSystem.Database;
    using System;

    class Startup
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Database.Migrate();

                DatabaseCommandEngine engine = new DatabaseCommandEngine(db);
            }
        }
    }
}
