namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using static Exceptions.ErrorMessages.BadRequestException;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, IHttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, IHttpCookie>();
        }

        public void Add(IHttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));
            this.cookies[cookie.Key] = cookie;
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));
            this.Add(new HttpCookie(key, value));
        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.cookies.ContainsKey(key);
        }

        public IHttpCookie Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.ContainsKey(key))
            {
                throw new InvalidOperationException(MissingKeyInHeaders);
            }

            return this.cookies[key];
        }

        public IEnumerator<IHttpCookie> GetEnumerator()
            => this.cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.cookies.Values.GetEnumerator();
    }
}
