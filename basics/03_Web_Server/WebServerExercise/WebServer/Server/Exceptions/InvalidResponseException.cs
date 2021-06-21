namespace WebServer.Server.Exceptions
{
    using System;

    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string errorMessage)
            : base(errorMessage) { }
    }
}
