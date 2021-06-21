namespace WebServer.LoginApplication.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using System.IO;
    using Views;

    public class UserController
    {
        private const string DefaultPath = @"../../../LoginApplication/Resources/Html/login.html";

        public IHttpResponse Login()
        {
            string result = File.ReadAllText(DefaultPath)
                .Replace(@"{{{contentDisplay}}}", "none")
                .Replace(@"{{{display}}}", "none");


            return new ViewResponse(HttpResponseStatusCode.OK, new LoginIndexView(result));
        }

        public IHttpResponse Login(string username, string password)
        {
            if (username != "suAdmin" && password != "aDminPw17")
            {
                string invalidUserResult = File.ReadAllText(DefaultPath)
                    .Replace(@"{{{message}}}", $"Invalid username or password!")
                .Replace(@"{{{contentDisplay}}}", "none")
                    .Replace(@"{{{display}}}", "block");

                return new ViewResponse(HttpResponseStatusCode.OK, new LoginIndexView(invalidUserResult));

            }

            string result = File.ReadAllText(DefaultPath)
                .Replace(@"{{{contentDisplay}}}", "block")
                .Replace(@"{{{display}}}", "none");

            return new ViewResponse(HttpResponseStatusCode.OK, new LoginIndexView(result));
        }
    }
}
