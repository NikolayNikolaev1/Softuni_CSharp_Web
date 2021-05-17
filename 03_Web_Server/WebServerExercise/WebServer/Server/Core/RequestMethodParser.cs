namespace WebServer.Server.Core
{
    using Enums;
    using Exceptions;
    using System;
    using System.Linq;

    using static Exceptions.ErrorMessages.BadRequestException;

    public static class RequestMethodParser
    {
        public static HttpRequestMethod Parse(string requestMethod)
        {
            //return Enum.Parse<HttpRequestMethod>(requestMethod, true); // One line solution..
            var requestMethodTypes = Enum
                .GetValues(typeof(HttpRequestMethod))
                .Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod methodType in requestMethodTypes)
            {
                if (methodType.ToString().Equals(requestMethod))
                {
                    return methodType;
                }
            }

            throw new BadRequestException(UnexistingRequestMethodType);
        }
    }
}
