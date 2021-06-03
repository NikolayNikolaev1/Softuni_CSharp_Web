namespace ByTheCake.Application.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WebServer.ByTheCakeApplication.Views;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    public abstract class Controller
    {
        protected const string DefaultPath = @"..\..\..\Resources\Html\{0}.html";
        protected const string DatabasePath = @"..\..\..\Data\database.csv";

        protected IDictionary<string, string> ViewData { get; private set; }
            = new Dictionary<string, string> { ["showLogout"] = "block" };

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

        private string ProccessFileHtml(string fileName)
        {
            string layoutHtml = File.ReadAllText(string.Format(DefaultPath, "Layout"));
            string fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));
            string result = layoutHtml.Replace("{{{content}}}", fileHtml);

            return result;
        }
    }
}
