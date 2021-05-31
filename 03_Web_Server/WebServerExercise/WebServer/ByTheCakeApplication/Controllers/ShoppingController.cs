namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Models;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using System.IO;
    using WebServer.Server.HTTP.Response;

    public class ShoppingController : Controller
    {
        private IList<Cake> cart = new List<Cake>();

        public IHttpResponse AddToCart(string id)
        {
            using (StreamReader reader = new StreamReader(DatabasePath))
            {
                string cake;
                while ((cake = reader.ReadLine()) != null)
                {
                    string[] cakeArgs = cake.Split(',');
                    string cakeId = cakeArgs[0];
                    string cakeName = cakeArgs[1];
                    string cakePrice = cakeArgs[2];

                    if (cakeId == id)
                    {
                        cart.Add(new Cake
                        {
                            Id = int.Parse(cakeId),
                            Name = cakeName,
                            Price = decimal.Parse(cakePrice)
                        });

                        break;
                    }
                }
            }

            this.ViewData["productCount"] = cart.Count.ToString();
            this.ViewData["showCart"] = "block";

            return this.FileViewResponse(@"cake\search");
        }
    }
}
