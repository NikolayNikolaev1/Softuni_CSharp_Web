namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System.IO;

    using static Server.Constants;

    public class HomeAboutView : IView
    {
        public string View()
            => File.ReadAllText(HtmlPath + "\\about.html");
    }
}
