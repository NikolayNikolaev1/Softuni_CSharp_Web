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
                .Get("/search", req => new CakeController().Search(req));
            appRouteConfig
                .Get("/login", req => new UserController().Login());
            appRouteConfig
                .Post("/login", req => new UserController().Login(req));
            appRouteConfig
                .Post("/logout", req => new UserController().Logout(req));
            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController().AddToCart(req));
            appRouteConfig
                .Get("/cart", req => new ShoppingController().Index(req));
            appRouteConfig
                .Get("/success", req => new ShoppingController().Success());
            appRouteConfig
                .Post("/order", req => new ShoppingController().Order(req));
        }
    }
}
