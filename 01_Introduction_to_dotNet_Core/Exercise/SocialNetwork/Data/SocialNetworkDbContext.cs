namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using SocialNetwork.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HP-ELITEBOOK\SQLEXPRESS;Database=StudentSystemDb;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Friendship>()
                .HasKey(f => new { f.FromUserId, f.ToUserId });

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.FromFriends)
                .WithOne(f => f.FromUser)
                .HasForeignKey(f => f.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.ToFriends)
                .WithOne(f => f.ToUser)
                .HasForeignKey(f => f.ToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
