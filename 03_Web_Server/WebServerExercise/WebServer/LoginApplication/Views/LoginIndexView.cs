namespace WebServer.LoginApplication.Views
{
    using Server.Contracts;

    public class LoginIndexView : IView
    {
        private string result;

        public LoginIndexView(string result)
        {
            this.result = result;
        }

        public string View()
        {
            return result;
        }
    }
}
