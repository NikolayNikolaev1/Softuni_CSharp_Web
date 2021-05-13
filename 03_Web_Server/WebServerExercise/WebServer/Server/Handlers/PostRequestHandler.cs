namespace WebServer.Server.Handlers
{
    using HTTP.Contracts;
    using System;

    public class PostRequestHandler : RequestHandler
    {
        public PostRequestHandler(Func<IHttpRequest, IHttpResponse> func)
            : base(func) { }
    }
}
