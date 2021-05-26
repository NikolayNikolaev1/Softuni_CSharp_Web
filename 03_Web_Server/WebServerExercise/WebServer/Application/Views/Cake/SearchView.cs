namespace WebServer.Application.Views.Cake
{
    using Server.Contracts;

    public class SearchView : IView
    {
        private readonly string htmlFile;

        public SearchView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
