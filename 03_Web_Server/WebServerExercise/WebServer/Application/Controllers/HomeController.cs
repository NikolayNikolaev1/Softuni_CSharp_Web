namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpResponseStatusCode.OK, new HomeIndexView());
        }
    }
}
