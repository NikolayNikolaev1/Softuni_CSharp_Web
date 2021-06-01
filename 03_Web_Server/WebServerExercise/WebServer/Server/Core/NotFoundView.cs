namespace WebServer.Server.Core
{
    using Contracts;

    public class NotFoundView : IView
    {
        public string View()
            => "<h1>404 - Not Found</h1>";
    }
}
