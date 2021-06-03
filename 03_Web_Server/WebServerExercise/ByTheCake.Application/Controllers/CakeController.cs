namespace ByTheCake.Application.Controllers
{
    using Models;
    using Core;
    using Infrastructure;
    using System.Collections.Generic;
    using System.IO;
    using System;
    using System.Text;
    using System.Linq;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.HTTP.Response;

    public class CakeController : Controller
    {
        private static IList<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";
            this.ViewData["showError"] = "none";
            return this.FileViewResponse(@"Cake\Add");
        }

        public IHttpResponse Add(IHttpRequest request)
        {
            const string formNameKey = "name";
            const string formPriceKey = "price";

            if (CoreValidator.CheckForMissingKeys(request, formNameKey, formPriceKey))
            {
                return new BadRequestResponse();
            }

            string name = request.FormData[formNameKey];
            string price = request.FormData[formPriceKey];

            if (CoreValidator.CheckIfNullOrEmpty(name, price))
            {
                this.ViewData["showResult"] = "none";
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "You have empty fields";

                return this.FileViewResponse(@"Cake\Add");
            }

            cakes.Add(new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            });

            StreamReader reader = new StreamReader(DatabasePath);
            int id = reader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Length;
            reader.Dispose();

            using (StreamWriter writer = new StreamWriter(DatabasePath, true))
            {
                writer.WriteLine($"{id},{name},{price}");
            }

            this.ViewData["showError"] = "none";
            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"Cake\Add");
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            IDictionary<string, string> parameters = request.QueryParameters;

            const string formSearchTermKey = "searchTerm";

            if (parameters.ContainsKey(formSearchTermKey))
            {
                string searchTerm = parameters[formSearchTermKey];

                StringBuilder result = new StringBuilder();
                string cakeName = string.Empty;
                string cakePrice = string.Empty;

                using (StreamReader reader = new StreamReader(DatabasePath))
                {
                    string cake;

                    while ((cake = reader.ReadLine()) != null)
                    {
                        string cakeId = cake.Split(',')[0];
                        cakeName = cake.Split(',')[1];
                        cakePrice = cake.Split(',')[2];

                        if (cakeName.Contains(searchTerm))
                        {
                            result.AppendLine($@"<div>{cakeName} ${cakePrice} <a href=""/shopping/add/{cakeId}"">Order</a></div>");
                        }
                    }
                }

                this.ViewData["result"] = result.ToString();
                this.ViewData["showCart"] = "none";
                this.ViewData["showResult"] = "block";

                return this.FileViewResponse(@"Cake\Search");
            }

            ShoppingCart cart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            this.ViewData["showCart"] = "none";
            this.ViewData["showResult"] = "none";

            if (cart.Orders.Any())
            {
                int totalProducts = cart.Orders.Count;
                string totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["productCount"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"Cake\Search");
        }
    }
}
