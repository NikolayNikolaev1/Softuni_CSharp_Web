namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }
    }
}
