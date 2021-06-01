namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;

    using static Server.Constants;

    public class UserController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["showLogout"] = "none";
            return this.FileViewResponse(@"user\login");
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!request.FormData.ContainsKey(formNameKey)
                || !request.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            string name = request.FormData[formNameKey];
            string password = request.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";
                return this.FileViewResponse(@"user\login");
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
