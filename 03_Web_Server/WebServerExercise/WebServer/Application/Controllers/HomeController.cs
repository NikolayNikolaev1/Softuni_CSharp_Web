namespace WebServer.Application.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index()
            => this.FileViewResponse("index");

        public IHttpResponse About()
            => this.FileViewResponse("about");
    }
}
