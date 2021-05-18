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
            this.routes = RequestMethodCollector.AddRequestMethodsToRoutes<RequestHandler>();
        }

        public IDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes
            => this.routes;

        public void AddRoute(string route, RequestHandler httpHandler)
        {
            CoreValidator.ThrowIfNullOrEmpty(route, nameof(route));
            CoreValidator.ThrowIfNull(httpHandler, nameof(httpHandler));

            bool requestExist = false;
            string handlerName = httpHandler
                   .GetType()
                   .ToString()
                   .ToLower();

            foreach (HttpRequestMethod requestMethod in RequestMethodCollector.MethodTypes)
            {
                if (handlerName.Contains(requestMethod.ToString().ToLower()))
                {
                    routes[requestMethod].Add(route, httpHandler);
                    requestExist = true;
                }
            }

            if (!requestExist)
            {
                throw new BadRequestException(UnexistingRequestMethodType);
            }
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
