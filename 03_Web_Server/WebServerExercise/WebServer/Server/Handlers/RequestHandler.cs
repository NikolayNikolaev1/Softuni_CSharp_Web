namespace WebServer.Server.Handlers
{
    using Contracts;
    using Core;
    using global::WebServer.Server.HTTP;
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
            string sessionIdToSend = null;
            IHttpResponse response = this.handlingFunc(httpContext.Request);

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
                response
                    .HeaderCollection
                    .Add(HeaderKeySetCookie, $"{SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");

            }



            foreach (IHttpCookie cookie in response.Cookies)
            {
                response.HeaderCollection.Add(HeaderKeySetCookie, cookie.ToString());
            }

            return httpResponse;
        }
    }
}
