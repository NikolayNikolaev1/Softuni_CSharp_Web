namespace WebServer.Application.Controllers
{
    using Server;
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

        public IHttpResponse Details(string name)
            => new ViewResponse(HttpResponseStatusCode.OK, new UserDetailsView(new Model { ["name"] = name }));
    }
}
