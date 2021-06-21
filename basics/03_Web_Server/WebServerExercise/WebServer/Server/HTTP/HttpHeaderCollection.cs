namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using static Exceptions.ErrorMessages.BadRequestException;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, ICollection<IHttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<IHttpHeader>>();
        }

        public void Add(IHttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            string headerKey = header.Key;

            if (!this.headers.ContainsKey(headerKey))
            {
                this.headers[headerKey] = new List<IHttpHeader>();
            }

            this.headers[headerKey].Add(header);
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));
            this.Add(new HttpHeader(key, value));
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            return this.headers.ContainsKey(key);
        }

        public IEnumerator<ICollection<IHttpHeader>> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.headers.Values.GetEnumerator();

        public ICollection<IHttpHeader> GetHeader(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException(MissingKeyInHeaders);
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var header in this.headers)
            {
                string headerKey = header.Key;

                foreach (IHttpHeader headerValue in header.Value)
                {
                    result.AppendLine($"{headerKey}: {headerValue.Value}");
                }
            }

            return result.ToString();
        }
    }
}
