namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using Enums;
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Net;

    using static Exceptions.ErrorMessages.BadRequestException;
    using static Constants;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));
            this.ParseRequest(requestString);
        }

        public IDictionary<string, string> FormData { get; private set; } 
            = new Dictionary<string, string>();

        public IHttpHeaderCollection HeaderCollection { get; private set; } 
            = new HttpHeaderCollection();

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; } 
            = new Dictionary<string, string>();

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; } 
            = new Dictionary<string, string>();

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            this.UrlParameters[key] = value;
        }

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

            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new[] { ": " }, StringSplitOptions.None);

                if (headerArgs.Length != HttpQueryLength)
                {
                    throw new BadRequestException(InvalidRequestLine);
                }

                string headerKey = headerArgs[0];
                string headerValue = headerArgs[1].Trim();

                IHttpHeader header = new HttpHeader(headerKey, headerValue);
                this.HeaderCollection.Add(header);
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
            CoreValidator.ThrowIfNullOrEmpty(requestMethod, nameof(requestMethod));
            return RequestMethodParser.Parse(requestMethod);
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
                        var queryKey = WebUtility.UrlDecode(queryArgs[0]);
                        var queryValue = WebUtility.UrlDecode(queryArgs[1]);
                        data.Add(queryKey, queryValue);
                    }
                }
            }
        }
    }
}
