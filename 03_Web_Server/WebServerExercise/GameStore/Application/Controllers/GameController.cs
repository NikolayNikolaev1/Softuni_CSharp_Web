namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using ViewModels.Game;
    using WebServer.Server.HTTP.Contracts;

    public class GameController : Controller
    {
        public IHttpResponse Details(IHttpRequest request)
        {
            int gameId = int.Parse(request.UrlParameters["id"]);

            GameDetailsViewModel model = this.GameService.Details(gameId);

            return this.FileViewResponse(@"game\details");
        }
    }
}
