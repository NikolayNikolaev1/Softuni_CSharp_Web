namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public User FindByUsername(string username)
            => this.context
            .Users
            .Where(u => u.Username == username)
            .Include(u => u.Orders)
            .FirstOrDefault();

    }
}
