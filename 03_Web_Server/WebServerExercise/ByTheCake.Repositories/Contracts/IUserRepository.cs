namespace ByTheCake.Providers.Contracts
{
    using Models;
    using Models.ViewModels;

    public interface IUserRepository : IRepository<User>
    {
        User Create(string username, string fullName, string password);

        User FindByUsername(string username);

        ProfileViewModel Profile(string username);
    }
}
