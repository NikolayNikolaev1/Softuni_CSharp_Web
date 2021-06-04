namespace ByTheCake.Application.Controllers
{
    using ByTheCake.Data;
    using Models;
    using Core;
    using Infrastructure;
    using Providers;
    using Providers.Contracts;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    using static Core.Constants.ClientErrorMessage;
    using static Core.Constants.FilePath;
    using static Core.Constants.ViewDataProperty;
    using ByTheCake.Models;

    public class CakeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CakeController(ByTheCakeDbContext dbContext)
        {
            this.unitOfWork = new UnitOfWork(dbContext);
            this.ViewData[Key.Error] = Value.None;
            this.ViewData[Key.Result] = Value.None;
            this.ViewData[Key.Cart] = Value.None;
        }

        public IHttpResponse Add()
            => this.FileViewResponse(CakeAdd);

        public IHttpResponse Add(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formPriceKey = "price";
            const string formPictureKey = "picture";

            if (CoreValidator.CheckForMissingKeys(request, formNameKey, formPriceKey, formPictureKey))
            {
                return new BadRequestResponse();
            }

            string name = request.FormData[formNameKey];
            string price = request.FormData[formPriceKey];
            string pictureUrl = request.FormData[formPictureKey];

            if (CoreValidator.CheckIfNullOrEmpty(name, price, pictureUrl))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, CakeAdd);
            }

            Product product = new Product
            {
                Name = name,
                Price = decimal.Parse(price),
                ImageUrl = pictureUrl
            };

            this.unitOfWork.ProductRepository.Add(product);
            this.unitOfWork.Save();

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData[Key.Result] = Value.Block;

            return this.FileViewResponse(CakeAdd);
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            int cakeId = int.Parse(request.UrlParameters["id"]);
            Product cake = unitOfWork
                .ProductRepository
                .Find(cakeId);

            if (cake == null)
            {
                return new BadRequestResponse();
            }

            this.ViewData["name"] = cake.Name;
            this.ViewData["price"] = cake.Price.ToString();
            this.ViewData["picture"] = cake.ImageUrl;

            return this.FileViewResponse(CakeDetails);
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            IDictionary<string, string> parameters = request.QueryParameters;

            const string formSearchTermKey = "searchTerm";

            if (parameters.ContainsKey(formSearchTermKey))
            {
                string searchTerm = parameters[formSearchTermKey];
                StringBuilder result = new StringBuilder();
                IList<Product> productsFound = this.unitOfWork
                    .ProductRepository
                    .Search(searchTerm)
                    .ToList();

                foreach (Product product in productsFound)
                {
                    result.AppendLine(
                        $@"<div><a href=""/cakeDetails/{product.Id}"">{product.Name}</a> ${product.Price} <a href=""/shopping/add/{product.Id}"">Order</a></div>");
                }

                this.ViewData["result"] = result.ToString();
                this.ViewData[Key.Result] = Value.Block;

                return this.FileViewResponse(CakeSearch);
            }

            ShoppingCart cart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (cart.Orders.Any())
            {
                int totalProducts = cart.Orders.Count;
                string totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData[Key.Cart] = Value.Block;
                this.ViewData["productCount"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(CakeSearch);
        }
    }
}
