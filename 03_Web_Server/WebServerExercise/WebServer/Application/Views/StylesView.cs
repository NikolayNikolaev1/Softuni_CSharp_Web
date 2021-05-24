namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System.IO;

    public class StylesView : IView
    {
        public string View()
        {
            return File.ReadAllText("..\\..\\..\\Application\\Resources\\css\\style.css");
        }
    }
}
