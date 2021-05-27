namespace WebServer.ByTheCakeApplication.Views.Cake
{
    using Server.Contracts;

    public class AddView : IView
    {
        private readonly string htmlFile;

        public AddView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
