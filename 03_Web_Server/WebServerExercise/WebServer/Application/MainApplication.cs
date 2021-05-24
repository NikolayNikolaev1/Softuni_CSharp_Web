namespace WebServer.Application
{
    using Controllers;
    using Server.Contracts;
    using Server.Handlers;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .AddRoute("/style.css", new GetRequestHandler(
                   httpContext => new HomeController().Styles()));


            appRouteConfig
                .AddRoute("/", new GetRequestHandler(
                    httpContext => new HomeController().Index()));
            appRouteConfig
                .AddRoute("/about", new GetRequestHandler(
                    httpContext => new HomeController().About()));
            appRouteConfig
                .AddRoute("/add", new GetRequestHandler(
                    httpContext => new CakeController().Add()));
            appRouteConfig
                .AddRoute("/search", new GetRequestHandler(
                    httpContext => new CakeController().Search()));
            appRouteConfig
                .AddRoute("/register", new PostRequestHandler(
                    httpContext => new UserController().RegisterPost(httpContext.FormData["name"])));
            appRouteConfig
                .AddRoute("/register", new GetRequestHandler(
                    httpContext => new UserController().RegisterGet()));
            appRouteConfig
                .AddRoute("/user/{(?<name>[a-z]+)}", new GetRequestHandler(
                    httpContext => new UserController().Details(httpContext.UrlParameters["name"])));
        }
    }
}
