namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views;

    public class HomeController
    {
        public IHttpResponse Index()
            => new ViewResponse(HttpResponseStatusCode.OK, new HomeIndexView());

        public IHttpResponse About()
            => new ViewResponse(HttpResponseStatusCode.OK, new HomeAboutView());


        public IHttpResponse Styles()
            => new ViewResponse(HttpResponseStatusCode.OK, new StylesView());
    }
}
