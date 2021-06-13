namespace GameStore.Application
{
    using Controllers;
    using Data;
    using GameStore.Application.ViewModels.Game.Admin;
    using System;
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
            appRouteConfig.Get(UrlPaths.Home,
                req => new HomeController().Index(req.Session));
            appRouteConfig.Get(UrlPaths.Login,
                req => new UserController().Login());
            appRouteConfig.Post(UrlPaths.Login,
                req => new UserController().Login(req.Session, new LoginUserViewModel
                {
                    Email = req.FormData["email"],
                    Password = req.FormData["password"]
                }));
            appRouteConfig.Get(UrlPaths.Logout,
                req => new UserController().Logout(req.Session));
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
            appRouteConfig.Get(UrlPaths.GameAdminList,
                req => new AdminController().AllGames(req.Session));
            appRouteConfig.Get(UrlPaths.GameAdd,
                req => new AdminController().AddGame(req.Session));
            appRouteConfig.Post(UrlPaths.GameAdd,
                req => new AdminController().AddGame(req.Session, new AddGameViewModel
                { 
                    Title = req.FormData["title"],
                    Description = req.FormData["description"],
                    ThumbnailUrl = req.FormData["thumbnail"],
                    Trailer = req.FormData["trailer"],
                    Price = req.FormData["price"],
                    Size = req.FormData["size"],
                    ReleaseDate = req.FormData["release-date"]
                }));
            appRouteConfig.Get(UrlPaths.GameEdit,
                req => new AdminController().EditGame(req));
            appRouteConfig.Get("/games",
                req => new GameController().Details(req));
        }

        private void InitializeDatabase()
        {
            using (GameStoreDbContext dbContext = new GameStoreDbContext())
            {
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
