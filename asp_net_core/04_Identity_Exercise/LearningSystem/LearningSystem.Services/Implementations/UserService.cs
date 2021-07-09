namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
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
    }
}
