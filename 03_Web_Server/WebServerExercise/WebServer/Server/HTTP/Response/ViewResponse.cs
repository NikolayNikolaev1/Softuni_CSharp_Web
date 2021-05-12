namespace WebServer.Server.HTTP.Response
{
    using WebServer.Server.Contracts;
    using WebServer.Server.Enums;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpResponseStatusCode responseCode, IView view)
            : base(responseCode, view) { }
    }
}
