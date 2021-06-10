namespace ByTheCake.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
