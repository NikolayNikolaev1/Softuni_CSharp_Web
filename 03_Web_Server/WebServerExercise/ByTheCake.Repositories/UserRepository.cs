namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.ViewModels;
    using System;
    using System.Linq;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public User Create(string username, string fullName, string password)
            => new User
            {
                Username = username,
                Name = fullName,
                Password = password,
                RegisterDate = DateTime.Now
            };

        public User FindByUsername(string username)
            => this.context
            .Users
            .Where(u => u.Username == username)
            .Include(u => u.Orders)
            .FirstOrDefault();

        public ProfileViewModel Profile(string username)
            => this.context
            .Users
            .Where(u => u.Username == username)
            .Select(u => new ProfileViewModel
            {
                Username = u.Username,
                FullName = u.Name,
                RegistrationDate = u.RegisterDate,
                TotalOrders = u.Orders.Count
            }).FirstOrDefault();
    }
}
