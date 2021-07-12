namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using LearningSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext dbContext;

        public UserService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<UserListingServiceModel> All()
            => this.dbContext
            .Users
            .ProjectTo<UserListingServiceModel>()
            .ToList();

        public bool SetRoles(string userId, ICollection<string> roleIds)
        {
            User user = this.dbContext
                .Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            ICollection<string> userRoleIds = user.Roles
                .Select(r => r.RoleId)
                .ToList();

            if (roleIds != null)
            {

                foreach (string existingRoleId in userRoleIds)
                {
                    bool isRoleRemoved = true;

                    foreach (string roleId in roleIds)
                    {
                        if (existingRoleId == roleId)
                        {
                            isRoleRemoved = false;
                            roleIds.Remove(roleId);
                            break;
                        }
                    }

                    if (isRoleRemoved)
                    {
                        user.Roles.Remove(user.Roles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == existingRoleId));
                    }
                }

                foreach (string roleId in roleIds)
                {

                    if (!userRoleIds.Contains(roleId))
                    {
                        user.Roles.Add(new UserRole
                        {
                            UserId = userId,
                            RoleId = roleId
                        });
                    }
                }
            }
            else
            {
                user.Roles.Clear();
            }

            this.dbContext.SaveChanges();
            return true;
        }
    }
}
