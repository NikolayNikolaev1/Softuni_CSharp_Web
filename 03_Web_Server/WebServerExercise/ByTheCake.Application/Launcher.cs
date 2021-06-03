namespace ByTheCake.Application
{
    using Infrastructure;
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
            IApplication app = new MainApplication();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            this.webServer = new WebServer(Port, routeConfig);
            this.webServer.Run();
        }
    }
}
