namespace ByTheCake.Application.Controllers
{
    using ByTheCake.Data;
    using ByTheCake.Models;
    using Data;
    using Infrastructure;
    using Models;
    using Providers;
    using Providers.Contracts;
    using System;
    using System.Linq;
    using System.Text;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static WebServer.Server.Constants;

    public class ShoppingController : Controller
    {
        private IUnitOfWork unityOfWork;
        private CakesData cakesData;

        public ShoppingController(ByTheCakeDbContext dbContext)
        {
            this.unityOfWork = new UnitOfWork(dbContext);
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            int cakeId = int.Parse(request.UrlParameters["id"]);
            Product cake = unityOfWork.ProductRepository.Find(cakeId);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Add(cake);

            return new RedirectResponse("/search");
        }

        public IHttpResponse Index(IHttpRequest request)
        {
            StringBuilder resultHtml = new StringBuilder();
            ShoppingCart cart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            decimal totalPrice = 0;

            if (!cart.Orders.Any())
            {
                this.ViewData["result"] = "No items in cart";
                this.ViewData["resultTotal"] = "0.00";
            }
            else
            {
                foreach (Product cake in cart.Orders)
                {
                    resultHtml.AppendLine($"<div>{cake.Name} - ${cake.Price}</div>");
                    totalPrice += cake.Price;
                }

                this.ViewData["result"] = resultHtml.ToString();
                this.ViewData["resultTotal"] = totalPrice.ToString();
            }

            return this.FileViewResponse(@"Order\Cart");
        }

        public IHttpResponse Success()
            => this.FileViewResponse(@"Order\Success");
    }
}
