namespace WebServer.Application.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using Models;

    public class CakeController : Controller
    {
        private IList<Cake> cakes;

        public IHttpResponse Add()
            => this.FileViewResponse("add-cake");
        public IHttpResponse Search()
            => this.FileViewResponse("search-cake");
    }
}
