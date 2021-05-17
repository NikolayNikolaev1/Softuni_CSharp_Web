namespace WebServer.Server.HTTP.Response
{
    using Core;
    using Enums;

    using static Constants;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
            : base() 
        {
            CoreValidator.ThrowIfNullOrEmpty(redirectUrl, nameof(redirectUrl));
            this.StatusCode = HttpResponseStatusCode.Found;
            this.AddHeader(HttpLocationHeader, redirectUrl);
        }
    }
}
