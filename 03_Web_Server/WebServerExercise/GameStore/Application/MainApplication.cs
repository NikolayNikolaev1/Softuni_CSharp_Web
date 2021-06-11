namespace GameStore.Application
{
    using Controllers;
    using GameStore.Data;
    using ViewModels.User;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing.Contracts;

    using static Infrastructure.Constants;

    public class MainApplication : IApplication
    {
        public MainApplication()
            => this.InitializeDatabase();

        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(UrlPaths.Login,
                req => new UserController().Login());
            appRouteConfig.Post(UrlPaths.Login,
                req => new UserController().Login(req.Session, new LoginUserViewModel
                {
                    Email = req.FormData["email"],
                    Password = req.FormData["password"]
                }));
            appRouteConfig.Get(UrlPaths.Register,
                req => new UserController().Register());
            appRouteConfig.Post(UrlPaths.Register,
                req => new UserController().Register(new RegisterUserViewModel
                {
                    Email = req.FormData["email"],
                    FullName = req.FormData["full-name"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));
        }

        private void InitializeDatabase()
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
