namespace GameStore.Application.Controllers
{
    using Infrastructure;
    using WebServer.Server.HTTP.Contracts;

    using static Infrastructure.Constants;

    public class HomeController : Controller
    {
        public IHttpResponse Index(IHttpSession session)
        {
            if (session.Containts(SessionKeys.CurrentUser))
            {
                string currentUserEmail = session
                    .GetParameter(SessionKeys.CurrentUser)
                    .ToString();
                this.ShowUserNavBar(currentUserEmail);
            }
            else
            {
                this.ShowGuestNavBar();
            }

            return this.FileViewResponse(FilePaths.HomeIndex);
        }
    }
}
