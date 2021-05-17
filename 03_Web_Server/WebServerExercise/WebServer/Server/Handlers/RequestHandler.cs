namespace WebServer.Server.Handlers
{
    using Contracts;
    using Core;
    using HTTP.Contracts;
    using System;

    using static Constants;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);
            httpResponse.AddHeader(HeaderKeyContentType, HeaderValueTextHtml);
            return httpResponse;
        }
    }
}
