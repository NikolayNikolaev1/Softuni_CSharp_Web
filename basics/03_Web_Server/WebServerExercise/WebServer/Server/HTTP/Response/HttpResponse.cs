namespace WebServer.Server.HTTP.Response
{
    using Contracts;
    using Core;
    using Enums;
    using System.Text;

    using static Constants;

    public abstract class HttpResponse : IHttpResponse
    {
        private HttpResponseStatusCode statusCode;
        private string statusMessage => this.statusCode.ToString();

        public IHttpCookieCollection Cookies { get; private set; } 
            = new HttpCookieCollection();

        public IHttpHeaderCollection HeaderCollection { get; private set; } 
            = new HttpHeaderCollection();

        public HttpResponseStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
            protected set
            {
                CoreValidator.ThrowIfNull(value, nameof(value));
                this.statusCode = value;
            }
        }

        public void AddHeader(string key, string redirectUrl) =>
            this.HeaderCollection.Add(new HttpHeader(key, redirectUrl));

        public override string ToString()
        {
            int statusCodeNumber = (int)this.StatusCode;
            StringBuilder response = new StringBuilder();

            response.AppendLine($"{HttpVersion.ToUpper()} {statusCodeNumber} {this.statusMessage}");
            response.AppendLine(this.HeaderCollection.ToString());

            return response.ToString();
        }
    }
}
