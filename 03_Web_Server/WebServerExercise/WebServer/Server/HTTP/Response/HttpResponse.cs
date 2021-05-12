namespace WebServer.Server.HTTP.Response
{
    using System.Text;
    using WebServer.Server.Contracts;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;

    using static Constants;

    public abstract class HttpResponse : IHttpeResponse
    {
        private readonly IView view;
        private HttpResponseStatusCode statusCode;

        protected HttpResponse(string redirectUrl)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpResponseStatusCode.Found;
            this.AddHeader(HttpLocationHeader, redirectUrl);
        }

        protected HttpResponse(HttpResponseStatusCode responseCode, IView view)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.view = view;
            this.StatusCode = responseCode;
        }

        public string Response
        {
            get
            {
                StringBuilder response = new StringBuilder();

                response.AppendLine($"{HttpVersion} {(int)this.StatusCode} {this.StatusMessage}");
                response.AppendLine(this.HeaderCollection.ToString());
                response.AppendLine();

                if ((int)this.statusCode < 300 || (int)this.statusCode >= 400)
                {
                    response.AppendLine(this.view.View());
                }

                return response.ToString();
            }
        }

        private IHttpHeaderCollection HeaderCollection { get; set; }

        private HttpResponseStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }
            set
            {
                this.statusCode = value;
            }
        }

        private string StatusMessage => this.statusCode.ToString();

        public void AddHeader(string key, string redirectUrl) =>
            this.HeaderCollection.Add(new HttpHeader(key, redirectUrl));
    }
}
