namespace CameraBazaar.App.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (StreamWriter writer = new StreamWriter("logs.txt", true))
            {
                DateTime dateTime = DateTime.UtcNow;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                string userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                string controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                string logMessage = $"{dateTime} - {ipAddress} - {userName} - {controller}.{action}";

                Exception exception = context.Exception;

                if (exception != null)
                {
                    logMessage = $"[!] {logMessage} - {exception.GetType().Name} - {exception.Message}";
                }

                writer.WriteLine(logMessage);
            }
        }
    }
}
