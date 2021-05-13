namespace WebServer.Server.Routing
{
    using Handlers.Contracts;
    using Routing.Contracts;
    using System.Collections.Generic;

    public class RoutingContext : IRoutingContext
    {
        public IEnumerable<string> Parameters { get; private set; } = new List<string>();

        public IRequestHandler RequestHandler { get; private set; }
    }
}
