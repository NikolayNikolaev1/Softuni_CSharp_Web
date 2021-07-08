namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Models.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext dbContext;

        public UserService(CameraBazaarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<UserListingServiceModel> All()
            => this.dbContext
            .Users
            .Select(u => new UserListingServiceModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                IsRestrict = u.IsRestrict
            }).ToList();

        public bool Allow(string userId)
        {
            var user = this.dbContext
                .Users
                .Find(userId);

            if (user == null)
            {
                return false;
            }

            user.IsRestrict = false;
            this.dbContext.SaveChanges();

            return true;
        }

        public bool IsRestrict(string userId)
            => this.dbContext
            .Users
            .Find(userId)
            .IsRestrict;

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

        public bool Restrict(string userId)
        {
            var user = this.dbContext
                .Users
                .Find(userId);

            if (user == null)
            {
                return false;
            }

            user.IsRestrict = true;
            this.dbContext.SaveChanges();

            return true;
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
