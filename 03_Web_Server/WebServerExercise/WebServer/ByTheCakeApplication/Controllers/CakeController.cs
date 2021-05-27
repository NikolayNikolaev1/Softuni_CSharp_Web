namespace WebServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.HTTP.Contracts;
    using System.Collections.Generic;
    using Models;
    using System.IO;
    using System.Text;

    public class CakeController : Controller
    {
        private static IList<Cake> cakes = new List<Cake>();
        private const string DatabasePath = @"..\..\..\ByTheCakeApplication\Data\database.csv";

        public IHttpResponse Add()
            => this.FileViewResponse(@"cake\add", new Dictionary<string, string>() { ["display"] = "none" });

        public IHttpResponse Add(string name, string price)
        {
            cakes.Add(new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            });

            using (StreamWriter writer = new StreamWriter(DatabasePath, true))
            {
                writer.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse(@"cake\add", new Dictionary<string, string>()
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });
        }

        public IHttpResponse Search(IDictionary<string, string> parameters)
        {
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
                        cakeName = cake.Split(',')[0];
                        cakePrice = cake.Split(',')[1];

                        if (cakeName.Contains(name))
                        {
                            result.AppendLine($"<div>{cakeName} ${cakePrice}</div>");
                        }
                    }
                }

                return this.FileViewResponse(@"cake\search", new Dictionary<string, string>()
                {
                    ["result"] = result.ToString(),
                    ["display"] = "block"
                });
            }

            return this.FileViewResponse(@"cake\search", new Dictionary<string, string>() { ["display"] = "none" });
        }
    }
}
