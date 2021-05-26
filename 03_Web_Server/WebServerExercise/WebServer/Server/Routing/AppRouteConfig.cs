namespace WebServer.Server.Routing
{
    using Contracts;
    using Core;
    using Enums;
    using Handlers;
    using HTTP.Contracts;
    using System;
    using System.Collections.Generic;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = RequestMethodCollector.AddRequestMethodsToRoutes<RequestHandler>();
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes
            => this.routes;

        public void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler)
            => this.routes[method].Add(route, httpHandler);

        public void Get(string route, Func<IHttpRequest, IHttpResponse> handler)
            => this.AddRoute(route, HttpRequestMethod.GET, new RequestHandler(handler));

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
            => this.AddRoute(route, HttpRequestMethod.POST, new RequestHandler(handler));
    }
}
