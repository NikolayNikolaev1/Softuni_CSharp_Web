namespace WebServer.Server.HTTP.Response
{
    using Enums;

    public class InternalServerErrorResponse : HttpResponse
    {
        public InternalServerErrorResponse()
        {
            this.StatusCode = HttpResponseStatusCode.InternalServerError;
        }
    }
}
