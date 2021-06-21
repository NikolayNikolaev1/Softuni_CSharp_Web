namespace WebServer.Server.HTTP.Response
{
    using Core;
    using Enums;
    using System;

    public class InternalServerErrorResponse : ViewResponse
    {
        public InternalServerErrorResponse(Exception exception, bool fullStackTrace = false)
            : base(HttpResponseStatusCode.InternalServerError, new InternalServerErrorView(exception)) { }
    }
}
