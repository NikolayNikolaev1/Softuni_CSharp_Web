namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using ViewModels.Game;
    using WebServer.Server.HTTP.Contracts;

    using static Infrastructure.Constants;

    public class GameController : Controller
    {
        public IHttpResponse Details(IHttpRequest request)
        {
            IHttpSession session = request.Session;

            if (session.Containts(SessionKeys.CurrentUser))
            {
                this.ShowUserNavBar(session.GetParameter(SessionKeys.CurrentUser).ToString());
            }
            else
            {
                this.ShowGuestNavBar();
            }

            int gameId = int.Parse(request.UrlParameters["id"]);

            GameDetailsViewModel model = this.GameService.Details(gameId);

            this.ViewData["gameId"] = gameId.ToString();
            this.ViewData["gameTitle"] = model.Title;
            this.ViewData["gameTrailer"] = model.Trailer;
            this.ViewData["gameDescription"] = model.Description;
            this.ViewData["gamePrice"] = model.Price.ToString("F2");
            this.ViewData["gameSize"] = model.Size.ToString("F1");
            this.ViewData["gameReleaseDate"] = model.ReleaseDate.ToString("dd/MM/yyyy");

            return this.FileViewResponse(@"game\details");
        }
    }
}
