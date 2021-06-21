namespace WebServer.CalculatorApplication.Views
{
    using Server.Contracts;
    using System.IO;

    public class CalculatorIndexView : IView
    {
        private string result;

        public CalculatorIndexView(string result)
        {
            this.result = result;
        }

        public string View()
            => this.result;
    }
}
