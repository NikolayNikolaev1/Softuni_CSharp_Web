﻿namespace WebServer.Server.Enums
{
    public enum HttpResponseStatusCode
    {
        OK = 200,
        MovedPermanently = 301,
        Found = 302,
        MovedTemporarily = 303,
        BadRequest = 400,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}
