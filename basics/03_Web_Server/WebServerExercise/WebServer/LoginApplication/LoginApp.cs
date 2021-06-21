namespace WebServer.LoginApplication
{
    using Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class LoginApp : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/login", req => new UserController().Login());
            appRouteConfig
                .Post("/login", req => new UserController()
                    .Login(req.FormData["username"], req.FormData["password"]));
        }
    }
}
