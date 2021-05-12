namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using WebServer.Server.HTTP.Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, IHttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, IHttpHeader>();
        }

        public void Add(IHttpHeader header) => this.headers.Add(header.Key, header);

        public bool ContainsKey(string key)
        {
            if (this.headers.ContainsKey(key))
            {
                return true;
            }

            return false;
        }

        public IHttpHeader GetHeader(string key)
        {
            if (this.ContainsKey(key))
            {
                return this.headers[key];
            }

            return null;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers);
        }
    }
}
