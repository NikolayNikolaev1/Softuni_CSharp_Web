namespace WebServer.Server.HTTP
{
    using Contracts;
    using Core;

    public class HttpHeader : IHttpHeader
    {
        private string key;
        private string value;

        public HttpHeader(string key, string value)
        {
            this.Key = key;
            this.Value = value;
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

        public override string ToString() 
            => string.Format($"{this.key}: {this.value}");
    }
}
