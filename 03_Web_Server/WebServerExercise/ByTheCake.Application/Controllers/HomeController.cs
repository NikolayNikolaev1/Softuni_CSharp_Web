namespace ByTheCake.Application.Controllers
{
    using Infrastructure;
    using WebServer.Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
            => this.FileViewResponse(@"Home\Index");

        public IHttpResponse About()
            => this.FileViewResponse(@"Home\About");
    }
}
