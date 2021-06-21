namespace ByTheCake.Models.ViewModels
{
    using ByTheCake.Models;
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "Current_Shopping_Cart";

        public IList<Product> Orders { get; set; } = new List<Product>();
    }
}
