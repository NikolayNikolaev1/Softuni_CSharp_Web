namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using System;
    using System.Collections.Generic;

    using static Exceptions.ErrorMessages.BadRequestException;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, IHttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, IHttpHeader>();
        }

        public void Add(IHttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.headers[header.Key] = header;
        } 

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            return this.headers.ContainsKey(key);
        }

        public IHttpHeader GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException(MissingKeyInHeaders);
            }

            return this.headers[key];
        }

        public override string ToString()
            => string.Join(Environment.NewLine, this.headers);
    }
}
