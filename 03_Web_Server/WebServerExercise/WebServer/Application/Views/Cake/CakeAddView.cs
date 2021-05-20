namespace WebServer.Application.Views.Cake
{
    using Server.Contracts;
    using System.IO;

    using static Server.Constants;

    public class CakeAddView : IView
    {
        public string View()
            => File.ReadAllText(HtmlPath + "\\cake\\add-cake.html");
    }
}
