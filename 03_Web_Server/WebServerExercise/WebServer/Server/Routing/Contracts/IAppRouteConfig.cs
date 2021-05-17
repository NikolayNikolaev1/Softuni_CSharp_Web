namespace WebServer.Server.Routing.Contracts
{
    using Enums;
    using Handlers;
    using System.Collections.Generic;

    public interface IAppRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void AddRoute(string route, RequestHandler httpHandler);
    }
}
