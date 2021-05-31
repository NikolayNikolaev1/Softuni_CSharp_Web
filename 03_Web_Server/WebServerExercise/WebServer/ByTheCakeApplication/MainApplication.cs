namespace WebServer.ByTheCakeApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index(req));
            appRouteConfig
                .Get("/about", req => new HomeController().About());
            appRouteConfig
                .Get("/add", req => new CakeController().Add());
            appRouteConfig
                .Post("/add", req => new CakeController().Add(req.FormData["name"], req.FormData["price"]));
            appRouteConfig
                .Get("/search", req => new CakeController().Search(req.QueryParameters));
            appRouteConfig
                .Get("/login", req => new UserController().Login());
            appRouteConfig
                .Post("/login", req => new UserController().Login(req));
            appRouteConfig
                .Get("/shopping/add/{{?<id>[0-9]+}}", req => new ShoppingController()
                    .AddToCart(req.UrlParameters["id"]));
        }
    }
}
