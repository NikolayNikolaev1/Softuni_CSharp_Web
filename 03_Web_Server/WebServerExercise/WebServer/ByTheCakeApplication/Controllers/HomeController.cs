namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;

    public class HomeController : Controller
    {
        public IHttpResponse Index(IHttpRequest request)
        {

            return this.FileViewResponse(@"home\index");
        }

        public IHttpResponse About()
            => this.FileViewResponse(@"home\about");
    }
}
