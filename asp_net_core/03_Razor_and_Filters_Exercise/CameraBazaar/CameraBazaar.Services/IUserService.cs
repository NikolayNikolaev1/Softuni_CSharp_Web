namespace CameraBazaar.Services
{
    using Models.User;

    public interface IUserService
    {
        UserProfileServiceModel Profile(string id);
    }
}
