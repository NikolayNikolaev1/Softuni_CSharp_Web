namespace WebServer.Server.Handlers
{
    using Handlers.Contracts;
    using HTTP.Contracts;
    using System;

    using static Constants;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func.Invoke(httpContext.Request);
            httpResponse.AddHeader(HeaderKeyContentType, HeaderValueTextHtml);
            return httpResponse;
        }
    }
}
