namespace GameStore.Application.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    public abstract class Controller
    {
        private const string DefaultPath = @"..\..\..\Application\Resources\Html\{0}.html";

        protected IDictionary<string, string> ViewData { get; private set; }
            = new Dictionary<string, string>();
        protected IHttpResponse FileViewResponse(string fileName)
        {
            string result = this.ProccessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var valuePair in ViewData)
                {
                    result = result.Replace($"{{{{{{{valuePair.Key}}}}}}}", valuePair.Value);
                }
            }

            return new ViewResponse(HttpResponseStatusCode.OK, new FileView(result));
        }

        protected IHttpResponse ErrorMessageResponse(string errorMessage, string filePath)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["errorMessage"] = errorMessage;

            return this.FileViewResponse(filePath);
        }

        private string ProccessFileHtml(string fileName)
        {
            string layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            string fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));
            string result = layoutHtml.Replace("{{{content}}}", fileHtml);

            return result;
        }
    }
}
