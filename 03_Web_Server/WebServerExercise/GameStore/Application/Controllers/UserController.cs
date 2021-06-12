namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using System.Linq;
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
            string email = model.Email;
            string password = model.Password;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                // Check for missing fields.
                return this.ErrorMessageResponse(ErrorMessages.EmptyFields, FilePaths.UserLogin);
            }

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
            string email = model.Email;
            string password = model.Password;
            string confirmPassword = model.ConfirmPassword;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(model.ConfirmPassword))
            {
                // Check for missing fields.
                return this.ErrorMessageResponse(ErrorMessages.EmptyFields, FilePaths.UserRegister);
            }

            if (!email.Contains('@') || !email.Contains('.'))
            {
                // Check for inavlid email.
                return this.ErrorMessageResponse(ErrorMessages.InvalidEmail, FilePaths.UserRegister);
            }

            if (!password.Any(l => char.IsUpper(l)) ||
                !password.Any(l => char.IsLower(l)) ||
                !password.Any(l => char.IsDigit(l)) ||
                password.Length < 6)
            {
                // Check for invalid password.
                return this.ErrorMessageResponse(ErrorMessages.InvalidPassword, FilePaths.UserRegister);
            }

            if (!password.Equals(confirmPassword))
            {
                // Check for invalid confirm password.
                return this.ErrorMessageResponse(ErrorMessages.InvalidConfirmPassword, FilePaths.UserRegister);
            }

            bool isUserCreated = this.UserService.Create(email, password, model.FullName);

            if (!isUserCreated)
            {
                // Check for existing user.
                return this.ErrorMessageResponse(ErrorMessages.UserExists, FilePaths.UserRegister);
            }

            return new RedirectResponse(UrlPaths.Login);
        }
    }
}
