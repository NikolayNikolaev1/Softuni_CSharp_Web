namespace ByTheCake.Providers.Contracts
{
    using Models;

    public interface IUserRepository : IRepository<User>
    {
        User FindByUsername(string username);
    }
}
