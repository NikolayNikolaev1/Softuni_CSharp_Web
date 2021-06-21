﻿namespace WebServer
{
    using CalculatorApplication;
    using LoginApplication;
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using Server.Routing.Contracts;
    using static Server.Constants;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main()
            => new Launcher().Run();

        public void Run()
        {
            IApplication app = new CalculatorApp();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            this.webServer = new WebServer(Port, routeConfig);
            this.webServer.Run();
        }
    }
}
