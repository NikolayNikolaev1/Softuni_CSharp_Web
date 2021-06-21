namespace WebServer.Server.Core
{
    using Enums;
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Exceptions.ErrorMessages.BadRequestException;

    public static class RequestMethodCollector
    {
        public static IList<HttpRequestMethod> MethodTypes = Enum
            .GetValues(typeof(HttpRequestMethod))
            .Cast<HttpRequestMethod>()
            .ToList();

        public static HttpRequestMethod Parse(string requestMethod)
        {
            //return Enum.Parse<HttpRequestMethod>(requestMethod, true); // One line solution..
            foreach (HttpRequestMethod methodType in MethodTypes)
            {
                if (methodType.ToString().Equals(requestMethod))
                {
                    return methodType;
                }
            }

            throw new BadRequestException(UnexistingRequestMethodType);
        }

        public static Dictionary<HttpRequestMethod, IDictionary<string, T>> AddRequestMethodsToRoutes<T>()
        {
            var routes = new Dictionary<HttpRequestMethod, IDictionary<string, T>>();

            MethodTypes
                .ToList()
                .ForEach(requestMethod
                    => routes[requestMethod] = new Dictionary<string, T>());

            return routes;
        }
    }
}
