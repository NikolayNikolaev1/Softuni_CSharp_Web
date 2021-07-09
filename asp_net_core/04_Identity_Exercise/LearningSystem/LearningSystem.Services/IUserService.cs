namespace LearningSystem.Services
{
    using Models.Users;
    using System.Collections.Generic;

    public interface IUserService
    {
        ICollection<UserListingServiceModel> All();
    }
}
