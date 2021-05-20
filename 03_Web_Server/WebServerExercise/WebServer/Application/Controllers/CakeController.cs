namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views.Cake;

    public class CakeController
    {
        public IHttpResponse Add()
            => new ViewResponse(HttpResponseStatusCode.OK, new CakeAddView());
        public IHttpResponse Search()
            => new ViewResponse(HttpResponseStatusCode.OK, new CakeSearchView());
    }
}
