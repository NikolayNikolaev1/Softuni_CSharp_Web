namespace ByTheCake.Application.Controllers
{
    using Infrastructure;
    using Models;
    using ByTheCake.Data;
    using ByTheCake.Models;
    using Providers;
    using Providers.Contracts;
    using System;
    using System.Linq;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static WebServer.Server.Constants;
    using System.Text;

    public class OrderController : Controller
    {
        private IUnitOfWork unityOfWork;

        public OrderController(ByTheCakeDbContext dbContext)
        {
            this.unityOfWork = new UnitOfWork(dbContext);
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            StringBuilder result = new StringBuilder();
            int orderId = int.Parse(request.UrlParameters["id"]);
            Order order = unityOfWork.OrderRepository.Find(orderId);

            if (order == null)
            {
                return new BadRequestResponse();
            }

            foreach (OrderProduct orderProduct in order.Products)
            {
                result.Append($@"
<tr>
    <th><a href=""/cakeDetails/{orderProduct.ProductId}"">{orderProduct.Product.Name}</a></th>
    <th>${order.Products.Sum(o => o.Product.Price * o.ProductCount)}</th>
</tr>");
            }

            this.ViewData["orderId"] = orderId.ToString();
            this.ViewData["result"] = result.ToString();
            this.ViewData["createdOn"] = order.CreationDate.ToString("dd-MM-yyyy");

            return this.FileViewResponse(@"Order\Details");
        }

        public IHttpResponse List(IHttpRequest request)
        {
            StringBuilder result = new StringBuilder();
            int currentUserId = request.Session.Get<User>(CurrentUserSessionKey).Id;
            var currentUser = unityOfWork.UserRepository.Find(currentUserId);
            var userOrders = currentUser
                .Orders
                .Select(o => new
            {
                OrderId = o.Id,
                CreatedOn = o.CreationDate.ToString("dd-MM-yyyy"),
                Sum = o.Products.Sum(p => p.Product.Price * p.ProductCount)
            }).OrderByDescending(o => o.CreatedOn)
            .ToList();

            foreach (var order in userOrders)
            {
                result.Append($@"
<tr>
    <th><a href=""/orderDetails/{order.OrderId}"">{order.OrderId}</a></th>
    <th>{order.CreatedOn}</th>
    <th>{order.Sum}</th>
</tr>");
            }

            this.ViewData["result"] = result.ToString();

            return this.FileViewResponse(@"Order\List");
        }

        public IHttpResponse Make(IHttpRequest request)
        {
            User currentUser = request.Session.Get<User>(CurrentUserSessionKey);
            ShoppingCart cart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            Order order = new Order
            {
                CreationDate = DateTime.Now,
            };
            currentUser.Orders.Add(order);

            foreach (Product product in cart.Orders)
            {
                var productOrderExist = order
                    .Products
                    .Where(op => op.OrderId == order.Id
                        && op.ProductId == product.Id);

                if (productOrderExist.Any())
                {
                    // Order with more than one of the same product.
                    productOrderExist
                        .FirstOrDefault()
                        .ProductCount++;

                    continue;
                }

                order.Products.Add(new OrderProduct
                {
                    Product = product,
                    Order = order,
                    ProductCount = 1
                });

                this.unityOfWork.OrderRepository.Add(order);
            }

            this.unityOfWork.Save();

            cart.Orders.Clear();

            return new RedirectResponse("/success");
        }

    }
}
