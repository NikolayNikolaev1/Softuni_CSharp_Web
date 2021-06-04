namespace ByTheCake.Application
{
    using ByTheCake.Data;
    using WebServer.Server;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing;
    using WebServer.Server.Routing.Contracts;

    using static WebServer.Server.Constants;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main()
            => new Launcher().Run();

        public void Run()
        {
            using (ByTheCakeDbContext dbContext = new ByTheCakeDbContext())
            {
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                IApplication app = new MainApplication(dbContext);
                IAppRouteConfig routeConfig = new AppRouteConfig();
                app.Start(routeConfig);

                this.webServer = new WebServer(Port, routeConfig);
                this.webServer.Run();
            }
        }
    }
}
