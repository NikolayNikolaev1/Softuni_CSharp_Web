namespace WebServer.Server.HTTP.Response
{
    using Enums;
    using Exceptions;
    using Server.Contracts;

    using static Exceptions.ErrorMessages.InvalidResponseException;
    public class ViewResponse : HttpResponse
    {
        private readonly IView view;

        public ViewResponse(HttpResponseStatusCode responseCode, IView view)
            : base() 
        {
            this.ValidateStatusCode(responseCode);
            this.StatusCode = responseCode;
            this.view = view;
        }

        private void ValidateStatusCode(HttpResponseStatusCode responseCode)
        {
            int statusCodeNumber = (int)responseCode;

            if (299 < statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException(InvalidStatusCode);
            }
        }

        public override string ToString()
            => $"{base.ToString()}{this.view.View()}";
    }
}
