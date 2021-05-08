namespace WebServer.Server.HTTP
{
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;

    using static Exceptions.ErrorMessages.BadRequestException;
    using static Constants;
    using System.Reflection;
    using System.Linq;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; } = new Dictionary<string, string>();

        public IHttpHeaderCollection HeaderCollection { get; private set; } = new HttpHeaderCollection();

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; } = new Dictionary<string, string>();

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; } = new Dictionary<string, string>();

        public void AddUrlParameter(string key, string value) => this.UrlParameters.Add(key, value);

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = requestLines[0]
                .Trim()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != HttpLength || requestLine[2].ToLower() != HttpVersion)
            {
                throw new BadRequestException(InvalidRequestLine);
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
        }

        private void ParseHeaders(string headers)
        {
            throw new NotImplementedException();
        }

        private void ParseParameters()
        {
            throw new NotImplementedException();
        }

        private HttpRequestMethod ParseRequestMethod(string requestMethod)
        {
            var type = Enum.GetValues(typeof(HttpRequestMethod));

            foreach (var item in type)
            {
                if (item.Equals(requestMethod))
                {
                    return (HttpRequestMethod)item;
                    // TODO
                }
            }

            throw new NotImplementedException();
        }
    }
}
