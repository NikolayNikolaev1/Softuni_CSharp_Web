namespace WebServer.Server.HTTP
{
    using Enums;
    using Exceptions;
    using HTTP.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using static Exceptions.ErrorMessages.BadRequestException;
    using static Constants;

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

            if (requestLine.Length != HttpRequestLength
                || requestLine[2].ToLower() != Constants.HttpVersion)
            {
                throw new BadRequestException(InvalidRequestLine);
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 0; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new[] { ": " }, StringSplitOptions.None);

                if (headerArgs.Length == 2)
                {

                    IHttpHeader header = new HttpHeader(headerArgs[0], headerArgs[1]);
                    this.HeaderCollection.Add(header);
                }
            }

            if (!this.HeaderCollection.ContainsKey(HttpHostHeader))
            {
                throw new BadRequestException(MissingHostHeader);
            }
        }

        private void ParseParameters()
        {
            if (this.Url.Contains("?"))
            {
                string query = this.Url
                    .Split('?')[1];
                this.ParseQuery(query, this.QueryParameters);
            }
        }

        private HttpRequestMethod ParseRequestMethod(string requestMethod)
        {
            var requestMethodTypes = Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>();

            foreach (HttpRequestMethod methodType in requestMethodTypes)
            {
                if (methodType.ToString().Equals(requestMethod))
                {
                    return methodType;
                }
            }

            throw new BadRequestException(UnexistingRequestMethodType);
        }

        private void ParseQuery(string query, IDictionary<string, string> data)
        {
            if (query.Contains("="))
            {
                string[] queryPairs = query
                    .Split('&');

                foreach (string queryPair in queryPairs)
                {
                    string[] queryArgs = queryPair
                        .Split('=');

                    if (queryArgs.Length == HttpQueryLength)
                    {
                        data.Add(WebUtility.UrlDecode(queryArgs[0]), WebUtility.UrlDecode(queryArgs[1]));
                    }
                }
            }
        }
    }
}
