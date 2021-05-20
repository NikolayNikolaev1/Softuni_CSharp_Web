namespace WebServer.Application.Views.Cake
{
    using Server.Contracts;
    using System.IO;

    using static Server.Constants;

    public class CakeSearchView : IView
    {
        public string View()
            => File.ReadAllText(HtmlPath + "\\cake\\search-cake.html");
    }
}
