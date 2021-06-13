namespace GameStore.Application.Controllers
{
    using Data.Models;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
            string trailer = model.Trailer;
            string thumbnailUrl = model.ThumbnailUrl;
            string description = model.Description;

            IDictionary<string, string> gameProperties = new Dictionary<string, string>
            {
                ["title"] = title,
                ["trailer"] = trailer,
                ["price"] = model.Price,
                ["size"] = model.Size,
                ["thumbnailUrl"] = thumbnailUrl,
                ["description"] = description
            };

            IDictionary<string, string> propertiesValidation = this.GamePropertiesValidation(gameProperties);

            if (propertiesValidation["isValid"].Equals("false"))
            {
                return this.ErrorMessageResponse(propertiesValidation["errorMessage"], FilePaths.GameAdd);
            }


            decimal price = decimal.Parse(model.Price);
            double size = double.Parse(model.Size);
            DateTime releaseDate = DateTime.Parse(model.ReleaseDate);//todo

            //if (string.IsNullOrEmpty(title) ||
            //    string.IsNullOrEmpty(price.ToString()) ||
            //    string.IsNullOrEmpty(size.ToString()))
            //{
            //    // Check for missing required fields.
            //    return this.ErrorMessageResponse(ErrorMessages.EmptyFields, FilePaths.GameAdd);
            //}

            //if (title.Length < 3 || title.Length > 100)
            //{
            //    // Check for invalid title length.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGameTitle, FilePaths.GameAdd);
            //}

            //if (price < 0)
            //{
            //    // Check for negative numbers.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGamePrice, FilePaths.GameAdd);
            //}

            //if (size < 0)
            //{
            //    // Check for negative numbers.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGameSize, FilePaths.GameAdd);
            //}

            //if (!string.IsNullOrEmpty(trailer) &&
            //    trailer.Contains('/') ||
            //    trailer.Contains('?') ||
            //    trailer.Contains('=') ||
            //    trailer.Length != 11)
            //{
            //    // Check for invalid trailer id.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGameTrailer, FilePaths.GameAdd);
            //}

            //if (!string.IsNullOrEmpty(thumbnailUrl) &&
            //    !thumbnailUrl.StartsWith("http://") &&
            //    !thumbnailUrl.StartsWith("https://"))
            //{
            //    // Check for invalid thumbnail url.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGameThumbnailUrl, FilePaths.GameAdd);
            //}

            //if (!string.IsNullOrEmpty(description) &&
            //    description.Length < 20)
            //{
            //    // Check for invalid description length.
            //    return this.ErrorMessageResponse(ErrorMessages.InvalidGameDescription, FilePaths.GameAdd);
            //}

            this.GameService.Create(title, price, size, trailer, thumbnailUrl, description, releaseDate);

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

            return new RedirectResponse(UrlPaths.GameAdminList);
        }

        private IDictionary<string, string> GamePropertiesValidation(IDictionary<string, string> properties)
        {
            string title = properties["title"];
            string trailer = properties["trailer"];
            decimal price = decimal.Parse(properties["price"]);
            double size = double.Parse(properties["size"]);
            string thumbnailUrl = properties["thumbnailUrl"];
            string description = properties["description"];

            if (string.IsNullOrEmpty(title) ||
                string.IsNullOrEmpty(price.ToString()) ||
                string.IsNullOrEmpty(size.ToString()))
            {
                // Check for missing required fields.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.EmptyFields
                };
            }

            if (title.Length < 3 || title.Length > 100)
            {
                // Check for invalid title length.

                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGameTitle
                };
            }

            if (price < 0)
            {
                // Check for negative numbers.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGamePrice
                };
            }

            if (size < 0)
            {
                // Check for negative numbers.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGameSize
                };
            }

            if (!string.IsNullOrEmpty(trailer) &&
                trailer.Contains('/') ||
                trailer.Contains('?') ||
                trailer.Contains('=') ||
                trailer.Length != 11)
            {
                // Check for invalid trailer id.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGameTrailer
                };
            }

            if (!string.IsNullOrEmpty(thumbnailUrl) &&
                !thumbnailUrl.StartsWith("http://") &&
                !thumbnailUrl.StartsWith("https://"))
            {
                // Check for invalid thumbnail url.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGameThumbnailUrl
                };
            }

            if (!string.IsNullOrEmpty(description) &&
                description.Length < 20)
            {
                // Check for invalid description length.
                return new Dictionary<string, string>
                {
                    ["isValid"] = "false",
                    ["errorMessage"] = ErrorMessages.InvalidGameDescription
                };
            }

            return new Dictionary<string, string>() { ["isValid"] = "true" };
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
