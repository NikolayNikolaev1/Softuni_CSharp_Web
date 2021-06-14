namespace GameStore.Application.Controllers
{
    using Data.Models;
    using Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ViewModels.Game.Admin;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static Infrastructure.Constants;

    public class AdminController : Controller
    {
        public AdminController()
        {
            this.HideErrorMessages();
        }

        public IHttpResponse AddGame(IHttpSession session)
        {
            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            return FileViewResponse(FilePaths.GameAdd);
        }

        public IHttpResponse AddGame(IHttpSession session, AddGameViewModel model)
        {
            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            string error = this.ValidateModel(model);
            if (error != null)
            {
                return this.ErrorMessageResponse(error, FilePaths.GameAdd);
            }

            this.GameService.Create(
                model.Title,
                model.Price,
                model.Size,
                model.Trailer,
                model.ThumbnailUrl,
                model.Description,
                model.ReleaseDate);

            return new RedirectResponse(UrlPaths.GameAdminList);
        }

        public IHttpResponse AllGames(IHttpSession session)
        {
            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            IList<GameListingViewModel> gameModels = this.GameService.All().ToList();
            StringBuilder tableHtml = new StringBuilder();

            for (int i = 0; i < gameModels.Count; i++)
            {
                tableHtml.AppendLine(@$"
<tr>
    <th scope=""row"">{i + 1}</th>
    <td>{gameModels[i].Title}</td>
    <td>{gameModels[i].Size} GB</td>
    <td>{gameModels[i].Price} &euro;</td>
    <td>
        <a class=""btn btn-warning"" href=""/game/edit/{gameModels[i].Id}"">Edit</button>
        <a class=""btn btn-danger"" href=""/game/delete/{gameModels[i].Id}"">Delete</button>
    </td>
</tr>");
            }

            this.ViewData["gamesTable"] = tableHtml.ToString();
            return this.FileViewResponse(FilePaths.GameList);
        }

        public IHttpResponse EditGame(IHttpRequest request)
        {
            IHttpSession session = request.Session;

            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            int gameId = int.Parse(request.UrlParameters["id"]);

            if (!this.GameService.Contains(gameId))
            {
                return new BadRequestResponse();
            }

            Game currentGame = this.GameService.Find(gameId);

            this.ViewData["gameTitle"] = currentGame.Title;
            this.ViewData["gameDescription"] = currentGame.Description;
            this.ViewData["gameThumbnail"] = currentGame.ImageThumbnail;
            this.ViewData["gamePrice"] = currentGame.Price.ToString("F2");
            this.ViewData["gameSize"] = currentGame.Size.ToString("F1");
            this.ViewData["gameTrailer"] = currentGame.Trailer;
            this.ViewData["gameReleaseDate"] = currentGame.ReleaseDate.ToString("dd-MM-yyyy");

            return this.FileViewResponse(FilePaths.GameEdit);
        }

        public IHttpResponse EditGame(IHttpSession session, EditGameViewModel model)
        {
            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            string error = this.ValidateModel(model);
            if (error != null)
            {
                return this.ErrorMessageResponse(error, FilePaths.UserRegister);
            }

            if (!this.GameService.Contains(model.Id))
            {
                return new BadRequestResponse();
            }

            this.GameService.Edit(
                model.Id,
                model.Title,
                model.Price,
                model.Size,
                model.Trailer,
                model.ThumbnailUrl,
                model.Description,
                model.ReleaseDate);

            return new RedirectResponse(UrlPaths.GameAdminList);
        }

        public IHttpResponse DeleteGame(IHttpRequest request)
        {
            if (!AdminAccess(request.Session))
            {
                return new BadRequestResponse();
            }

            int gameId = int.Parse(request.UrlParameters["id"]);

            if (!this.GameService.Contains(gameId))
            {
                return new BadRequestResponse();
            }

            this.GameService.Delete(gameId);

            return new RedirectResponse(UrlPaths.GameAdminList);
        }

        private bool AdminAccess(IHttpSession session)
        {
            if (!session.Containts(SessionKeys.CurrentUser))
            {
                // Check if user is logged in.
                return false;
            }

            string currentUserEmail = session
                .GetParameter(SessionKeys.CurrentUser)
                .ToString();
            bool isAdmin = this.UserService.IsAdmin(currentUserEmail);

            if (!this.UserService.IsAdmin(currentUserEmail))
            {
                // Check if user is admin.
                return false;
            }

            this.ShowUserNavBar(currentUserEmail);
            return true;
        }
    }
}
