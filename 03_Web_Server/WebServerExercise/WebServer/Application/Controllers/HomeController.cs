namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Application.Views;

    public class HomeController
    {
        public IHttpResponse Index()
            => new ViewResponse(HttpResponseStatusCode.OK, new HomeIndexView());
    }
}
