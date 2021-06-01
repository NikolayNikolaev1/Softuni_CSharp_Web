namespace WebServer.Server.HTTP.Response
{
    using Core;
    using Enums;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
            : base(HttpResponseStatusCode.NotFound, new NotFoundView()) { }
    }
}
