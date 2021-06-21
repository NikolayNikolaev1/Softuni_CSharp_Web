namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;
    using System;

    public class HttpCookie : IHttpCookie
    {
        private string key;
        private string value;
        private DateTime expires;
        private bool isNew;

        public HttpCookie(string key, string value, int expires = 3)
        {
            this.Key = key;
            this.Value = value;
            this.Expires = DateTime.UtcNow.AddDays(expires);
            this.IsNew = true;
        }

        public HttpCookie(string key, string value, bool isNew, int expires = 3)
            : this(key, value, expires)
        {
            this.IsNew = isNew;
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            private set
            {
                CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
                this.key = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            private set
            {
                CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
                this.value = value;
            }
        }

        public DateTime Expires
        {
            get
            {
                return this.expires;
            }
            private set
            {
                CoreValidator.ThrowIfNull(value, nameof(value));
                this.expires = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            private set
            {
                CoreValidator.ThrowIfNull(value, nameof(value));
                this.isNew = value;
            }
        }

        public override string ToString()
            => $"{this.key}={this.value}; Expires={this.expires.ToLongTimeString()}";
    }
}
