namespace WebServer.Server
{
    using Contracts;
    using Server.Routing;
    using Routing.Contracts;
    using System;
    using System.Net;
    using System.Net.Sockets;

    using static Constants;

    class WebServer : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunning;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse(IpAddress), port);
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
