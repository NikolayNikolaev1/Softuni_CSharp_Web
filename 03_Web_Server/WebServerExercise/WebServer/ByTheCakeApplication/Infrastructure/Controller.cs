namespace WebServer.ByTheCakeApplication.Infrastructure
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Views;

    public abstract class Controller
    {
        public const string DefaultPath = @"..\..\..\ByTheCakeApplication\Resources\html\{0}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        public IHttpResponse FileViewResponse(string fileName)
        {
            string result = this.ProcessFileHtml(fileName);

            return new ViewResponse(HttpResponseStatusCode.OK, new FileView(result));
        }

        public IHttpResponse FileViewResponse(string fileName, IDictionary<string, string> values)
        {
            string result = this.ProcessFileHtml(fileName);

            if (values != null && values.Any())
            {
                foreach (var value in values)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpResponseStatusCode.OK, new FileView(result));
        }

        private string ProcessFileHtml(string fileName)
        {
            string layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            string fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));
            string result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
