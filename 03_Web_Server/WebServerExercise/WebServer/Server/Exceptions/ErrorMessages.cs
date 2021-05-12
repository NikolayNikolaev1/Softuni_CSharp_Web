namespace WebServer.Server.Exceptions
{
    public static class ErrorMessages
    {
        public static class BadRequestException
        {
            public const string InvalidRequestLine = "Invalid request line!";

            public const string MissingHostHeader = "There is no host header!";

            public const string UnexistingRequestMethodType = "Request method type does not exist!";
        }
    }
}
