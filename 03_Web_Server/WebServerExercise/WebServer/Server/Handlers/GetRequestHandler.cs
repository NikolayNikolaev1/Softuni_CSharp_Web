namespace WebServer.Server.Handlers
{
    using HTTP.Contracts;
    using System;

    public class GetRequestHandler : RequestHandler
    {
        public GetRequestHandler(Func<IHttpRequest, IHttpResponse> func)
            : base(func) { }
    }
}
