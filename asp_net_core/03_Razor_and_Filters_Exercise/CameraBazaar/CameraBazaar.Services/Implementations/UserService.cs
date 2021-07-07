namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Models.User;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext dbContext;

        public UserService(CameraBazaarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserProfileServiceModel Profile(string id)
        {
            var user = this.dbContext
                .Users
                .Where(u => u.Id.Equals(id));

            if (user == null)
            {
                return null;
            }

            return user
                .Select(u => new UserProfileServiceModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    CamerasInStockCount = u.Cameras.Where(c => c.Quantity > 0).Count(),
                    CamerasOutOfStockCount = u.Cameras.Where(c => c.Quantity == 0).Count(),
                    LastLoginTime = u.LastLoginTime
                }).FirstOrDefault();
        }

        public void SetLastLoginTime(string username)
        {
            this.dbContext
                .Users
                .FirstOrDefault(u => u.UserName.Equals(username))
                .LastLoginTime = DateTime.UtcNow;
            this.dbContext.SaveChanges();
        }
    }
}
