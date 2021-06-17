namespace SimpleMvc.App
{
    using Framework;
    using Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main(string[] args)
            => MvcEngine.Run(
                new WebServer(8080,
                    new ControllerRouter()));
    }
}
