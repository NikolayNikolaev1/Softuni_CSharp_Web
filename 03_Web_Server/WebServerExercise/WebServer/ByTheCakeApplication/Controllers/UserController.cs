namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views;

    using static Server.Constants;

    public class UserController : Controller
    {
        public IHttpResponse RegisterGet()
            => new ViewResponse(HttpResponseStatusCode.OK, new RegisterView());

        public IHttpResponse RegisterPost(string name)
            => new RedirectResponse($"/user/{name}");

        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
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

            return new RedirectResponse("/");
        }
    }
}
