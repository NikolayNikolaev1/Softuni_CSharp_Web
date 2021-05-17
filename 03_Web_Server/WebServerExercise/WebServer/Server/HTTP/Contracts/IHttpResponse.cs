namespace WebServer.Server.HTTP.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        IHttpHeaderCollection HeaderCollection { get; }

        HttpResponseStatusCode StatusCode { get; }

        void AddHeader(string key, string redirectUrl);
    }
}
