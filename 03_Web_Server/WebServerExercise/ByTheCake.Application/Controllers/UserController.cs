namespace ByTheCake.Application.Controllers
{
    using Core;
    using Infrastructure;
    using Models;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static WebServer.Server.Constants;

    public class UserController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["showLogout"] = "none";

            return this.FileViewResponse(@"User\Login");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";


            if (CoreValidator.CheckForMissingKeys(request, formNameKey, formPasswordKey))
            {
                return new BadRequestResponse();
            }

            string name = request.FormData[formNameKey];
            string password = request.FormData[formPasswordKey];

            if (CoreValidator.CheckIfNullOrEmpty(name, password))
            {

                this.ViewData["showLogout"] = "none";
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "You have empty fields";

                return this.FileViewResponse(@"User\Login");
            }

            request.Session.Add(CurrentUserSessionKey, name);
            request.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();
            return new RedirectResponse("/login");
        }
    }
}
