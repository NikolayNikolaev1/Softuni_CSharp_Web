namespace WebServer.Server.HTTP.Contracts
{
    public interface IHttpeResponse
    {
        string Response { get; }

        void AddHeader(string key, string redirectUrl);
    }
}
