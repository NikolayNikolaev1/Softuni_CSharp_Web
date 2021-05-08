namespace WebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using WebServer.Server.HTTP.Contracts;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public HttpHeader GetHeader(string key)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers);
        }
    }
}
