namespace ByTheCake.Application
{
    using Controllers;
    using Data;
    using Models.ViewModels;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        ByTheCakeDbContext context;

        public MainApplication(ByTheCakeDbContext context)
        {
            this.context = context;
        }

        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());
            appRouteConfig
                .Get("/about", req => new HomeController().About());
            appRouteConfig
                .Get("/register", req => new UserController(this.context).Register());
            appRouteConfig
                .Post("/register", req => new UserController(this.context)
                    .Register(req, new RegisterUserViewModel
                    { 
                        Username = req.FormData["username"],
                        FullName = req.FormData["full-name"],
                        Password = req.FormData["password"],
                        ConfirmPassword = req.FormData["confirm-password"]
                    }));
            appRouteConfig
                .Get("/login", req => new UserController(this.context).Login());
            appRouteConfig
                .Post("/login", req => new UserController(this.context)
                    .Login(req, new LoginUserViewModel
                    {
                        Username = req.FormData["username"],
                        Password = req.FormData["password"]
                    }));
            appRouteConfig
                .Post("/logout", req => new UserController(this.context).Logout(req));
            appRouteConfig
                .Get("/profile", req => new UserController(this.context).Profile(req));
            appRouteConfig
                .Get("/add", req => new CakeController(this.context).Add());
            appRouteConfig
                .Post("/add", req => new CakeController(this.context)
                    .Add(req, new CreateCakeViewModel
                    { 
                        Name = req.FormData["name"],
                        Price = decimal.Parse(req.FormData["price"]),
                        PictureUrl = req.FormData["picture"]
                    }));
            appRouteConfig
                .Get("/cakeDetails/{(?<id>[0-9]+)}", req => new CakeController(this.context).Details(req));
            appRouteConfig
                .Get("/search", req => new CakeController(this.context).Search(req));
            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController(this.context).AddToCart(req));
            appRouteConfig
                .Get("/cart", req => new ShoppingController(this.context).Index(req));
            appRouteConfig
                .Get("/success", req => new ShoppingController(this.context).Success());
            appRouteConfig
                .Post("/order", req => new OrderController(this.context).Make(req));
            appRouteConfig
                .Get("/orders", req => new OrderController(this.context).List(req));
            appRouteConfig
                .Get("/orderDetails/{(?<id>[0-9]+)}", req => new OrderController(this.context).Details(req));
        }
    }
}
