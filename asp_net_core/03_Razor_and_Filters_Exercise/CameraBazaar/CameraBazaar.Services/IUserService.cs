namespace CameraBazaar.Services
{
    using Models.User;
    using System.Collections.Generic;

    public interface IUserService
    {
        ICollection<UserListingServiceModel> All();

        bool Allow(string userId);

        bool IsRestrict(string userId);

        UserProfileServiceModel Profile(string id);

        bool Restrict(string userId);

        void SetLastLoginTime(string username);
    }
}
