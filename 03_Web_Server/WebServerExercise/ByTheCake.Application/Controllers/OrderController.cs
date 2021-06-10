namespace ByTheCake.Application.Controllers
{
    using Data;
    using Infrastructure;
    using Models;
    using Models.ViewModels;
    using Providers;
    using Providers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static WebServer.Server.Constants;

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
            OrderDetailsViewModel orderModel = this.unityOfWork
                .OrderRepository
                .Details(orderId);

            if (orderModel == null)
            {
                return new BadRequestResponse();
            }

            foreach (Product product in orderModel.Products)
            {

                result.Append($@"
<tr>
    <th><a href=""/cakeDetails/{product.Id}"">{product.Name}</a></th>
    <th>${product.Price.ToString("F2")}</th>
</tr>");
            }


            this.ViewData["orderId"] = orderId.ToString();
            this.ViewData["result"] = result.ToString();
            this.ViewData["createdOn"] = orderModel.CreatedOn.ToString("dd-MM-yyyy");

            return this.FileViewResponse(@"Order\Details");
        }

        public IHttpResponse List(IHttpRequest request)
        {
            StringBuilder result = new StringBuilder();
            string currentUserName = request
                .Session
                .GetParameter(CurrentUserSessionKey)
                .ToString();

            User currentUser = this.unityOfWork
                .UserRepository
                .FindByUsername(currentUserName);

            IList<OrderListingViewModel> ordersModel = this.unityOfWork
                .OrderRepository
                .GetAll(currentUser.Id)
                .ToList();

            foreach (var order in ordersModel)
            {
                result.Append($@"
<tr>
    <th><a href=""/orderDetails/{order.Id}"">{order.Id}</a></th>
    <th>{order.CreatedOn.ToString("dd-MM-yyyy")}</th>
    <th>${order.TotalSum.ToString("F2")}</th>
</tr>");
            }

            this.ViewData["result"] = result.ToString();

            return this.FileViewResponse(@"Order\List");
        }

        public IHttpResponse Make(IHttpRequest request)
        {
            string currentUserName = request
                .Session
                .GetParameter(CurrentUserSessionKey)
                .ToString();
            User currentUser = this.unityOfWork
                .UserRepository
                .FindByUsername(currentUserName);
            ShoppingCart cart = request
                .Session
                .Get<ShoppingCart>(ShoppingCart.SessionKey);

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
