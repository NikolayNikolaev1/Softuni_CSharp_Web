namespace WebServer.Server.Handlers
{
    using Contracts;
    using Core;
    using HTTP;
    using HTTP.Contracts;
    using System;

    using static Constants;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            string sessionIdToSend = null;
            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);
            httpResponse.AddHeader(HeaderKeyContentType, HeaderValueTextHtml);

            if (!httpContext.Request.Cookies.ContainsKey(SessionCookieKey))
            {
                string sessionId = Guid.NewGuid().ToString();
                httpContext.Request.Session = SessionStore.Get(sessionId);
                sessionIdToSend = sessionId;
            }

            if (sessionIdToSend != null)
            {
                httpResponse
                    .HeaderCollection
                    .Add(HeaderKeySetCookie, $"{SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }

            foreach (IHttpCookie cookie in httpResponse.Cookies)
            {
                httpResponse.HeaderCollection.Add(HeaderKeySetCookie, cookie.ToString());
            }

            return httpResponse;
        }
    }
}
