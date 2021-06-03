namespace ByTheCake.Application.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using System.Linq;
    using System.Text;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    public class ShoppingController : Controller
    {
        private CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            int id = int.Parse(request.UrlParameters["id"]);
            Cake cake = this.cakesData.Find(id);

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
            decimal totalPrice = 0;
            ShoppingCart cart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);


            if (!cart.Orders.Any())
            {
                this.ViewData["result"] = "No items in cart";
                this.ViewData["resultTotal"] = $"Total Cost: $0.00";
            }
            else
            {
                foreach (Cake cake in cart.Orders)
                {
                    resultHtml.AppendLine($"<div>{cake.Name} - ${cake.Price}</div>");
                    totalPrice += cake.Price;
                }
                this.ViewData["result"] = resultHtml.ToString();
                this.ViewData["resultTotal"] = $"Total Cost: ${totalPrice}";
            }


            return this.FileViewResponse(@"Order\Cart");
        }

        public IHttpResponse Order(IHttpRequest request)
        {
            request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();
            return new RedirectResponse("/success");
        }

        public IHttpResponse Success()
            => this.FileViewResponse(@"Order\Success");
    }
}
