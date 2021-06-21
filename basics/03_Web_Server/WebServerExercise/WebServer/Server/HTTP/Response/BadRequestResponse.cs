namespace WebServer.Server.HTTP.Response
{
    using Enums;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = HttpResponseStatusCode.BadRequest;
        }
    }
}
