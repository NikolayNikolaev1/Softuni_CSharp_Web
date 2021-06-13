namespace GameStore.Application.Infrastructure
{
    using GameStore.Services;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using WebServer.Server.Enums;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    public abstract class Controller
    {
        private const string DefaultPath = @"..\..\..\Application\Resources\Html\{0}.html";

        protected IDictionary<string, string> ViewData { get; private set; }
            = new Dictionary<string, string>();

        protected IGameService GameService { get; private set; } = new GameService();

        protected IUserService UserService { get; private set; } = new UserService();

        protected IHttpResponse FileViewResponse(string fileName)
        {
            string result = this.ProccessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var valuePair in ViewData)
                {
                    result = result.Replace($"{{{{{{{valuePair.Key}}}}}}}", valuePair.Value);
                }
            }

            return new ViewResponse(HttpResponseStatusCode.OK, new FileView(result));
        }

        protected IHttpResponse ErrorMessageResponse(string errorMessage, string filePath)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["errorMessage"] = errorMessage;

            return this.FileViewResponse(filePath);
        }

        protected void HideErrorMessages()
        {
            this.ViewData["showError"] = "none";
        }

        protected void ShowGuestNavBar()
        {
            this.ViewData["navigationBar"] = @"
<li class=""nav - item"">
    <a class=""nav-link"" href=""/login"">Login</a>
</li>
<li class=""nav - item"">
    <a class=""nav-link"" href=""/register"">Register</a>
</li>";
        }

        protected void ShowUserNavBar(string userEmail)
        {
            StringBuilder navbarHtml = new StringBuilder();

            if (this.UserService.IsAdmin(userEmail))
            {
                // Add adming button for admins.
                navbarHtml.Append(@"
<li class=""nav-item dropdown"">
        <a class=""nav-link dropdown-toggle"" href=""#"" id=""navbarDropdown"" role=""button"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
          Admin
        </a>
    <div class=""dropdown-menu"" aria-labelledby=""navbarDropdown"">
        <a class=""dropdown-item"" href=""/game/add"">Add Game</a>
        <a class=""dropdown-item"" href=""/admin/games"">All Games</a>
    </div>
</li>");


            }

            navbarHtml.Append(@"
<li class=""nav - item"">
    <a class=""nav-link"" href=""/logout"">Logout</a>
</li>");

            this.ViewData["navigationBar"] = navbarHtml.ToString();
        }

        private string ProccessFileHtml(string fileName)
        {
            string layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            string fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));
            string result = layoutHtml.Replace("{{{content}}}", fileHtml);

            return result;
        }
    }
}
