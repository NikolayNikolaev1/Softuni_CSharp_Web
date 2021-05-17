namespace WebServer.Server.Routing
{
    using Contracts;
    using Core;
    using Enums;
    using Exceptions;
    using Handlers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Exceptions.ErrorMessages.BadRequestException;

    public class AppRouteConfig : IAppRouteConfig
    {
        private readonly IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, IDictionary<string, RequestHandler>>();
            this.AddRequestMethodsToRoutes();
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes
            => this.routes;

        public void AddRoute(string route, RequestHandler httpHandler)
        {
            CoreValidator.ThrowIfNullOrEmpty(route, nameof(route));
            CoreValidator.ThrowIfNull(httpHandler, nameof(httpHandler));

            var handlerName = httpHandler
                .GetType()
                .ToString()
                .ToLower();

            if (handlerName.Contains("get"))
            {
                this.routes[HttpRequestMethod.GET].Add(route, httpHandler);
            }
            else if (handlerName.Contains("post"))
            {
                this.routes[HttpRequestMethod.POST].Add(route, httpHandler);
            }
            else
            {
                throw new BadRequestException(UnexistingRequestMethodType);
            }
            // TODO : Use reflection

            //var requestmethod = RequestMethodParser.Parse()


            //var methodTypes = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            //foreach (HttpRequestMethod requestMethod in methodTypes)
            //{
            //    if (requestMethod.Equals(httpHandler))
            //    {
            //        this.routes[requestMethod].Add(route, httpHandler);
            //    }
            //}
        }

        private void AddRequestMethodsToRoutes()
        {
            var methodTypes = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod requestMethod in methodTypes)
            {
                this.routes[requestMethod] = new Dictionary<string, RequestHandler>();
            }
        }
    }
}
