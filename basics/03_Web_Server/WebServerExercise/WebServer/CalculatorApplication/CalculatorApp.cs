namespace WebServer.CalculatorApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class CalculatorApp : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/calculator", req => new CalculatorController().Index());
            appRouteConfig
                .Post("/calculator", req => new CalculatorController()
                    .Calculate(req.FormData["first-number"], req.FormData["second-number"], req.FormData["sign"]));
        }
    }
}
