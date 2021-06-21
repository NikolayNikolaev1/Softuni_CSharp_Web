namespace StudentSystem.Database
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;

    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HP-ELITEBOOK\SQLEXPRESS;Database=StudentSystemDb;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder
                .Entity<Student>()
                .HasMany(s => s.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(s => s.StudentId);

            modelBuilder
                .Entity<Course>()
                .HasMany(c => c.Studens)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder
                .Entity<Course>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            modelBuilder
                .Entity<Course>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            modelBuilder
                .Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);

            modelBuilder
                .Entity<License>()
                .HasOne(l => l.Resource)
                .WithMany(r => r.Licenses)
                .HasForeignKey(l => l.ResourceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
