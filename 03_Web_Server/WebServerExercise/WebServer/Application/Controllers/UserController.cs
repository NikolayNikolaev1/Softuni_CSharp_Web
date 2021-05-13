namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views;

    public class UserController
    {
        public IHttpResponse RegisterGet()
            => new ViewResponse(HttpResponseStatusCode.OK, new RegisterView());

        public IHttpResponse RegisterPost(string name)
            => new RedirectResponse($"/user/{name}");
    }
}
