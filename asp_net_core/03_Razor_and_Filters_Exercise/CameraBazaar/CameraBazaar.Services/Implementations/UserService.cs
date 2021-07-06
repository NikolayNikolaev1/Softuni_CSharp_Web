namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Models.User;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext dbContext;

        public UserService(CameraBazaarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserProfileServiceModel Profile(string id)
        {
            var user = this.dbContext
                .Users
                .Where(u => u.Id.Equals(id));

            if (user == null)
            {
                return null;
            }

            return user
                .Select(u => new UserProfileServiceModel
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    //CamerasInStockCount = 
                }).FirstOrDefault();
        }
    }
}
