namespace WebServer.Server.HTTP
{
    using Contracts;
    using System.Collections.Concurrent;

    public static class SessionStore
    {
        private static readonly ConcurrentDictionary<string, IHttpSession> sessions =
            new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession Get(string id)
            => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}
