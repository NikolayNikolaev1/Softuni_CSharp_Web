namespace WebServer.Server.Exceptions
{
    public static class ErrorMessages
    {
        public static class BadRequestException
        {
            public const string InvalidRequestLine = "Invalid request line!";

            public const string MissingHostHeader = "There is no host header!";

            public const string MissingKeyInCookies = "The given key is not present in the cookies collection!";

            public const string MissingKeyInHeaders = "The given key is not present in the headers collection!";

            public const string UnexistingRequestMethodType = "Request method type does not exist!";
        }

        public static class InvalidResponseException
        {
            public const string InvalidStatusCode = "View response need a status code between 300 and 400(inclusive)!";
        }
    }
}
