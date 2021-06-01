namespace WebServer.Server.Handlers
{
    using Contracts;
    using Core;
    using Enums;
    using HTTP.Contracts;
    using HTTP.Response;
    using Routing.Contracts;
    using System;
    using System.Text.RegularExpressions;

    using static Constants;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));
            this.serverRouteConfig = serverRouteConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            try
            {
                string loginPath = "/login";

                if (httpContext.Request.Path != loginPath &&
                    !httpContext.Request.Session.Containts(CurrentUserSessionKey))
                {
                    return new RedirectResponse(loginPath);
                }

                HttpRequestMethod requestMethod = httpContext.Request.RequestMethod;
                string requestPath = httpContext.Request.Path;
                var registeredRoutes = this.serverRouteConfig.Routes[requestMethod];

                foreach (var registeredRoute in registeredRoutes)
                {
                    string routePattern = registeredRoute.Key;
                    IRoutingContext routingContext = registeredRoute.Value;

                    Regex regex = new Regex(routePattern);
                    Match match = regex.Match(requestPath);


                    if (!match.Success)
                    {
                        continue;
                    }

                    var parameters = routingContext.Parameters;

                    foreach (string parameter in parameters)
                    {

                        string parameterValue = match.Groups[parameter].Value;
                        httpContext.Request.AddUrlParameter(parameter, parameterValue);
                    }

                    return routingContext.RequestHandler.Handle(httpContext);
                }
            }
            catch (Exception ex)
            {
                return new InternalServerErrorResponse(ex);
            }

            return new NotFoundResponse();
        }
    }
}
