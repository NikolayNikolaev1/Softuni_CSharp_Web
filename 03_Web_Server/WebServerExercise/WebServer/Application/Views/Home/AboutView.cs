namespace WebServer.Application.Views
{
    using Server.Contracts;

    public class AboutView : IView
    {
        private readonly string htmlFile;

        public AboutView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
