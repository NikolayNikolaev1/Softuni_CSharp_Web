namespace GameStore.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        public bool Create(string email, string password, string fullName = null)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                var users = dbContext.Users;

                if (!users.Any())
                {
                    // Create Admin if it is the first User.
                    users.Add(new User
                    {
                        Email = email,
                        Password = password,
                        FullName = fullName,
                        IsAdmin = true
                    });

                    dbContext.SaveChanges();
                    return true;
                }

                if (!users.Any(u => u.Email.Equals(email)))
                {
                    // Check for not existing user email.
                    users.Add(new User
                    {
                        Email = email,
                        Password = password,
                        FullName = fullName,
                        IsAdmin = false
                    });

                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool IsAdmin(string email)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                return dbContext
                    .Users
                    .Where(u => u.Email.Equals(email))
                    .FirstOrDefault()
                    .IsAdmin;
            }
        }

        public bool Login(string email, string password)
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                return dbContext
                    .Users
                    .Any(u => u.Email.Equals(email) 
                    && u.Password.Equals(password));
            }
        }
    }
}
