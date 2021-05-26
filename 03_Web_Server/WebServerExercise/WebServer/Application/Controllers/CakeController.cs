namespace WebServer.Application.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;

    public class CakeController : Controller
    {
        public IHttpResponse Add()
            => this.FileViewResponse("add-cake");
        public IHttpResponse Search()
            => this.FileViewResponse("search-cake");
    }
}
