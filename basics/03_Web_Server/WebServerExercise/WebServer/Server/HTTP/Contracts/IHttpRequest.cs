namespace WebServer.Server.HTTP.Contracts
{
    using Enums;
    using System.Collections.Generic;

    public interface IHttpRequest
    {
        IHttpCookieCollection Cookies { get; }

        IDictionary<string, string> FormData { get; }

        IHttpHeaderCollection HeaderCollection { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpSession Session { get; set; }

        string Url { get; }

        IDictionary<string, string> UrlParameters { get; }

        void AddUrlParameter(string key, string value);
    }
}
