﻿namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using Models;
    using System.IO;
    using System.Text;
    using System;
    using System.Linq;

    public class CakeController : Controller
    {
        private static IList<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";
            return this.FileViewResponse(@"cake\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            cakes.Add(new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            });

            StreamReader reader = new StreamReader(DatabasePath);
            int id = reader.ReadToEnd().Split(Environment.NewLine).Length;
            reader.Dispose();

            using (StreamWriter writer = new StreamWriter(DatabasePath, true))
            {
                writer.WriteLine($"{id},{name},{price}");
            }

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"cake\add");
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            IDictionary<string, string> parameters = request.QueryParameters;

            if (parameters.ContainsKey("name"))
            {
                string name = parameters["name"];

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

                        if (cakeName.Contains(name))
                        {
                            result.AppendLine($@"<div>{cakeName} ${cakePrice} <a href=""/shopping/add/{cakeId}"">Order</a></div>");
                        }
                    }
                }

                this.ViewData["result"] = result.ToString();
                this.ViewData["showCart"] = "none";
                this.ViewData["display"] = "block";

                return this.FileViewResponse(@"cake\search");
            }

            var shoppingCart = request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            this.ViewData["showCart"] = "none";
            this.ViewData["display"] = "none";

            if (shoppingCart.Orders.Any())
            {
                int totalProducts = shoppingCart.Orders.Count;
                string totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["productCount"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"cake\search");
        }
    }
}
