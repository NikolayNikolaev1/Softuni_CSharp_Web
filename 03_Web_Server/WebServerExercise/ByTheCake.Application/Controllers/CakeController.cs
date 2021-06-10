namespace ByTheCake.Application.Controllers
{
    using Core;
    using Data;
    using Infrastructure;
    using Models;
    using Models.ViewModels;
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

        public IHttpResponse Add(IHttpRequest request, CreateCakeViewModel model)
        {
            const string formNameKey = "name";
            const string formPriceKey = "price";
            const string formPictureKey = "picture";

            if (CoreValidator.CheckForMissingKeys(request, formNameKey, formPriceKey, formPictureKey))
            {
                return new BadRequestResponse();
            }

            if (CoreValidator.CheckIfNullOrEmpty(model.Name, model.Price.ToString(), model.PictureUrl))
            {
                return this.ReturnResponseWithErrorMessage(EmptyFields, CakeAdd);
            }

            Product product = this.unitOfWork
                .ProductRepository
                .Create(model.Name, model.Price, model.PictureUrl);
            this.unitOfWork.ProductRepository.Add(product);
            this.unitOfWork.Save();

            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = model.Price.ToString(); ;
            this.ViewData[Key.Result] = Value.Block;

            return this.FileViewResponse(CakeAdd);
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            int cakeId = int.Parse(request.UrlParameters["id"]);
            CakeDetailsViewMovdel cakeModel = this.unitOfWork
                .ProductRepository
                .Details(cakeId);

            if (cakeModel == null)
            {
                return new BadRequestResponse();
            }

            this.ViewData["name"] = cakeModel.Name;
            this.ViewData["price"] = cakeModel.Price.ToString();
            this.ViewData["picture"] = cakeModel.PictureUrl;

            return this.FileViewResponse(CakeDetails);
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            IDictionary<string, string> parameters = request.QueryParameters;

            const string formSearchTermKey = "searchTerm";

            string searchTerm = request.QueryParameters.ContainsKey(formSearchTermKey)
                ? request.QueryParameters[formSearchTermKey] 
                : null;

            StringBuilder result = new StringBuilder();
            IList<ProductListingViewModel> productsModel = this.unitOfWork
                .ProductRepository
                .Search(searchTerm)
                .ToList();

            foreach (ProductListingViewModel product in productsModel)
            {
                result.AppendLine(
                    $@"<div><a href=""/cakeDetails/{product.Id}"">{product.Name}</a> ${product.Price} <a href=""/shopping/add/{product.Id}"">Order</a></div>");
            }

            this.ViewData["result"] = result.ToString();
            this.ViewData[Key.Result] = Value.Block;

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
