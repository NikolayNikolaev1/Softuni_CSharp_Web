namespace WebServer.Server.HTTP
{
    using WebServer.Server.HTTP.Contracts;

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
                this.value = value;
            }
        }

        public override string ToString()
        {
            return string.Format($"{this.key}: {this.value}");
        }
    }
}
