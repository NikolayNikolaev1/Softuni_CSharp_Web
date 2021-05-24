namespace WebServer.Server.HTTP.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        void Add(string key, string value);

        void Clear();

        object GetParameter(string key);

        T Get<T>(string key) where T : class;

        bool IsAuthenticated();
    }
}
