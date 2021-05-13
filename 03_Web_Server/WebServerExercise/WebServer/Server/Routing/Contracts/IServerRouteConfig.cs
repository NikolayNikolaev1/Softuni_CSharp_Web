namespace WebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Enums;

    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }
    }
}
