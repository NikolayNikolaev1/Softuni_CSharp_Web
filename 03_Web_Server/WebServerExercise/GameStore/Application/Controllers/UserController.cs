namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using ViewModels.User;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static Infrastructure.Constants;

    public class UserController : Controller
    {

        public UserController()
        {
            this.ShowGuestNavBar();
            this.HideErrorMessages();
        }

        public IHttpResponse Login()
            => this.FileViewResponse(FilePaths.UserLogin);

        public IHttpResponse Login(IHttpSession session, LoginUserViewModel model)
        {
            string error = this.ValidateModel(model);
            if (error != null)
            {
                return this.ErrorMessageResponse(error, FilePaths.UserRegister);
            }

            string email = model.Email;
            string password = model.Password;

            if (!this.UserService.Login(email, password))
            {
                // Check for not existing user.
                return this.ErrorMessageResponse(ErrorMessages.UserNotExist, FilePaths.UserLogin);
            }

            session.Add(SessionKeys.CurrentUser, email);
            this.ShowUserNavBar(email);

            return new RedirectResponse(UrlPaths.Home);
        }

        public IHttpResponse Logout(IHttpSession session)
        {
            session.Clear();
            return new RedirectResponse(UrlPaths.Home);
        }

        public IHttpResponse Register()
            => this.FileViewResponse(FilePaths.UserRegister);

        public IHttpResponse Register(RegisterUserViewModel model)
        {
            string error = this.ValidateModel(model);
            if (error != null)
            {
                return this.ErrorMessageResponse(error, FilePaths.UserRegister);
            }

            bool isUserCreated = this.UserService.Create(model.Email, model.Password, model.FullName);

            if (!isUserCreated)
            {
                // Check for existing user.
                return this.ErrorMessageResponse(ErrorMessages.UserExists, FilePaths.UserRegister);
            }

            return new RedirectResponse(UrlPaths.Login);
        }
    }
}
