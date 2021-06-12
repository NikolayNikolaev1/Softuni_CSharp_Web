namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using System.Collections.Generic;
    using ViewModels.Game.Admin;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static GameStore.Application.Infrastructure.Constants;

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

            string title = model.Title;
            string trailer = model.Trailer.Substring(model.Trailer.Length - 11);
            decimal price = model.Price;
            double size = model.Size;
            string thumbnailUrl = model.ThumbnailUrl;
            string description = model.Description;

            if (string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(price.ToString()) ||
                string.IsNullOrEmpty(size.ToString()))
            {
                // Check for missing required fields.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            if (title.Length < 3 || title.Length > 100)
            {
                // Check for invalid title length.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            if (price < 0 || size < 0)
            {
                // Check for negative numbers.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            if (!string.IsNullOrEmpty(trailer) &&
                trailer.Contains('/') ||
                trailer.Contains('?') ||
                trailer.Contains('=') ||
                trailer.Length != 11)
            {
                // Check for invalid trailer id.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            if (!string.IsNullOrEmpty(thumbnailUrl) &&
                !thumbnailUrl.StartsWith("http://") ||
                !thumbnailUrl.StartsWith("https://"))
            {
                // Check for invalid thumbnail url.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            if (!string.IsNullOrEmpty(description) &&
                description.Length < 20)
            {
                // Check for invalid description length.
                return this.ErrorMessageResponse("error", FilePaths.GameAdd);
            }

            this.GameService.Create(title, price, size, trailer, thumbnailUrl, description);

            return new RedirectResponse(UrlPaths.GameAdminList);
        }

        public IHttpResponse AllGames(IHttpSession session)
        {
            if (!AdminAccess(session))
            {
                return new BadRequestResponse();
            }

            ICollection<GameListingViewModel> gameModels = this.GameService.All();

            foreach (GameListingViewModel model in gameModels)
            {

            }

            return this.FileViewResponse(FilePaths.GameList);
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
