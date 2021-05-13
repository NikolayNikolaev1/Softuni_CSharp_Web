namespace WebServer.Server.Routing
{
    using Enums;
    using Handlers;
    using Routing.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AppRouteConfig : IAppRouteConfig
    {
        //private readonly IDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.Routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();
            this.AddRequestMethodsToRoutes();
        }

        public IDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes { get; private set; }
            = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();

        public void AddRoute(string route, RequestHandler httpHandler)
        {
            var methodTypes = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod requestMethod in methodTypes)
            {
                this.Routes[requestMethod].Add(route, httpHandler);
            }
        }

        private void AddRequestMethodsToRoutes()
        {
            var methodTypes = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod requestMethod in methodTypes)
            {
                this.Routes.Add(requestMethod, new Dictionary<string, RequestHandler>());
            }
        }
    }
}
