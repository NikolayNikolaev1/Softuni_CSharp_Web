namespace WebServer.Application.Infrastructure
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using System.IO;
    using Views;

    public abstract class Controller
    {
        public const string DefaultPath = @"..\\..\\..\\Application\\Resources\\html\\{0}.html";

        public IHttpResponse FileViewResponse(string fileName)
        {
            string layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            string fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));
            string result = layoutHtml.Replace("{{{content}}}", fileHtml);

            return new ViewResponse(HttpResponseStatusCode.OK, new FileView(result));
        }
    }
}
