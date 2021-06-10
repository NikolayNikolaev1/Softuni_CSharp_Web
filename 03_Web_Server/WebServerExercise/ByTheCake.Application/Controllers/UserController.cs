namespace ByTheCake.Application.Controllers
{
    using Core;
    using Data;
    using Infrastructure;
    using Models;
    using Models.ViewModels;
    using Providers;
    using Providers.Contracts;
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

        public IHttpResponse Login(IHttpRequest request, LoginUserViewModel model)
        {
            if (CoreValidator.CheckForMissingKeys(request, FormUserNameKey, FormPasswordKey))
            {
                return new BadRequestResponse();
            }

            if (CoreValidator.CheckIfNullOrEmpty(model.Username, model.Password))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, UserLogin);
            }

            User currentUser = this.unitOfWork
                .UserRepository
                .FindByUsername(model.Username);

            if (currentUser == null || currentUser.Password != model.Password)
            {
                return this.ReturnResponseWithErrorMessage("User does not exist. Register a new user.", UserLogin);
            }

            request.Session.Add(CurrentUserSessionKey, model.Username);
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

        public IHttpResponse Register(IHttpRequest request, RegisterUserViewModel model)
        {
            if (CoreValidator.CheckForMissingKeys(request, FormUserNameKey, FormFullNameKey, FormPasswordKey, FormConfirmPasswordKey))
            {
                return new BadRequestResponse();
            }

            if (CoreValidator.CheckIfNullOrEmpty(model.Username, model.FullName, model.Password, model.ConfirmPassword))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, UserRegister);
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.ReturnResponseWithErrorMessage("Pasword and Confirm Password does not match.", UserRegister);
            }

            if (this.unitOfWork.UserRepository.FindByUsername(model.Username) != null)
            {
                return this.ReturnResponseWithErrorMessage("Username already exists.", UserRegister);
            }

            User user = this.unitOfWork
                .UserRepository
                .Create(model.Username, model.FullName, model.Password);
            this.unitOfWork.UserRepository.Add(user);
            this.unitOfWork.Save();

            request.Session.Add(CurrentUserSessionKey, model.Username);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public IHttpResponse Profile(IHttpRequest request)
        {
            string currentUserName = request.Session.GetParameter(CurrentUserSessionKey).ToString();
            ProfileViewModel model = this.unitOfWork
                .UserRepository
                .Profile(currentUserName);

            this.ViewData[Key.Logout] = Value.Block;
            this.ViewData["username"] = model.Username;
            this.ViewData["fullName"] = model.FullName;
            this.ViewData["registeredOn"] = model.RegistrationDate.ToString("dd-MM-yyyy");
            this.ViewData["ordersCount"] = model.TotalOrders.ToString();

            return this.FileViewResponse(UserProfile);
        }
    }
}
