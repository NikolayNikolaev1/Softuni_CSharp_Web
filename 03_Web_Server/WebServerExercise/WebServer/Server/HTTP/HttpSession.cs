namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using System;
    using System.Collections.Generic;

    using static Exceptions.ErrorMessages.BadRequestException;

    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> values;
        private string id;

        public HttpSession(string id)
        {
            this.Id = id;
            this.values = new Dictionary<string, object>();
        }


        public string Id
        {
            get
            {
                return this.id;
            }
            private set
            {
                CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
                this.id = value;
            }
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            this.values[key] = value;
        }

        public void Clear()
            => this.values.Clear();

        public bool Containts(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.values.ContainsKey(key);
        }

        public T Get<T>(string key) where T : class
            => this.GetParameter(key) as T;

        public object GetParameter(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (!this.values.ContainsKey(key))
            {
                throw new InvalidOperationException(MissingKeyInCookies);
            }

            return this.values[key];
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }
    }
}
