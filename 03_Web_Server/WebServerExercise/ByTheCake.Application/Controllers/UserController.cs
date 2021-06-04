namespace ByTheCake.Application.Controllers
{
    using ByTheCake.Data;
    using ByTheCake.Models;
    using Core;
    using Infrastructure;
    using Models;
    using Providers;
    using Providers.Contracts;
    using System;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static Core.Constants.ClientErrorMessage;
    using static Core.Constants.FilePath;
    using static Core.Constants.ViewDataProperty;
    using static WebServer.Server.Constants;

    public class UserController : Controller
    {
        private const string FormUserNameKey = "username";
        private const string FormFullNameKey = "full-name";
        private const string FormPasswordKey = "password";
        private const string FormConfirmPasswordKey = "confirm-password";
        private IUnitOfWork unitOfWork;

        public UserController(ByTheCakeDbContext dbContext)
        {
            this.unitOfWork = new UnitOfWork(dbContext);
            this.ViewData[Key.Logout] = Value.None;
            this.ViewData[Key.Error] = Value.None;
        }

        public IHttpResponse Login()
            => this.FileViewResponse(UserLogin);

        public IHttpResponse Login(IHttpRequest request)
        {
            if (CoreValidator.CheckForMissingKeys(request, FormUserNameKey, FormPasswordKey))
            {
                return new BadRequestResponse();
            }

            string username = request.FormData[FormUserNameKey];
            string password = request.FormData[FormPasswordKey];

            if (CoreValidator.CheckIfNullOrEmpty(username, password))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, UserLogin);
            }

            User currentUser = this.unitOfWork.UserRepository.FindByUsername(username);

            if (currentUser == null || currentUser.Password != password)
            {
                return this.ReturnResponseWithErrorMessage("User does not exist. Register a new user.", UserLogin);
            }

            request.Session.Add(CurrentUserSessionKey, currentUser);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();
            return new RedirectResponse("/login");
        }

        public IHttpResponse Register()
            => this.FileViewResponse(UserRegister);

        public IHttpResponse Register(IHttpRequest request)
        {
            if (CoreValidator.CheckForMissingKeys(request, FormUserNameKey, FormFullNameKey, FormPasswordKey, FormConfirmPasswordKey))
            {
                return new BadRequestResponse();
            }

            string username = request.FormData[FormUserNameKey];
            string fullName = request.FormData[FormFullNameKey];
            string password = request.FormData[FormPasswordKey];
            string confirmPassword = request.FormData[FormConfirmPasswordKey];

            if (CoreValidator.CheckIfNullOrEmpty(username, fullName, password, confirmPassword))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, UserRegister);
            }

            if (password != confirmPassword)
            {
                return this.ReturnResponseWithErrorMessage("Pasword and Confirm Password does not match.", UserRegister);
            }

            if (this.unitOfWork.UserRepository.FindByUsername(username) != null)
            {
                return this.ReturnResponseWithErrorMessage("Username already exists.", UserRegister);
            }

            User user = new User
            {
                Username = username,
                Name = fullName,
                Password = password,
                RegisterDate = DateTime.Now
            };

            this.unitOfWork.UserRepository.Add(user);

            this.unitOfWork.Save();

            request.Session.Add(CurrentUserSessionKey, user);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public IHttpResponse Profile(IHttpRequest request)
        {
            User currentUser = request.Session.Get<User>(CurrentUserSessionKey);

            this.ViewData[Key.Logout] = Value.Block;
            this.ViewData["username"] = currentUser.Username;
            this.ViewData["fullName"] = currentUser.Name;
            this.ViewData["registeredOn"] = currentUser.RegisterDate.ToString("dd-MM-yyyy");
            this.ViewData["ordersCount"] = currentUser.Orders.Count.ToString();

            return this.FileViewResponse(UserProfile);
        }
    }
}
