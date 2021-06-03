namespace ByTheCake.Application.Infrastructure
{
    using Controllers;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());
            appRouteConfig
                .Get("/about", req => new HomeController().About());
            appRouteConfig
                .Get("/login", req => new UserController().Login());
            appRouteConfig
                .Post("/login", req => new UserController().Login(req));
            appRouteConfig
                .Post("/logout", req => new UserController().Logout(req));
            appRouteConfig
                .Get("/add", req => new CakeController().Add());
            appRouteConfig
                .Post("/add", req => new CakeController().Add(req));
            appRouteConfig
                .Get("/search", req => new CakeController().Search(req));
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
