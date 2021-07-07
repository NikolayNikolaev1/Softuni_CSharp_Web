namespace CameraBazaar.App.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Diagnostics;
    using System.IO;

    public class MeasureTimeAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopwatch.Stop();

            using (StreamWriter writer = new StreamWriter("action-times.txt", true))
            {
                DateTime dateTime = DateTime.UtcNow;
                string controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                string logMessage = $"{dateTime} - {controller}.{action} - {this.stopwatch.Elapsed}";

                writer.WriteLine(logMessage);
            }
        }
    }
}
