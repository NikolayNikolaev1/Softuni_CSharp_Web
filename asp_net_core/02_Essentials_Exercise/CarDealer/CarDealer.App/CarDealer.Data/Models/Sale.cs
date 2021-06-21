namespace CarDealer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Range(0, 100)]
        // In percentage.
        public int Discount { get; set; }
    }
}
