namespace GameStore.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string email, string password, string fullName);

        bool IsAdmin(string email);

        bool Login(string email, string password);
    }
}
