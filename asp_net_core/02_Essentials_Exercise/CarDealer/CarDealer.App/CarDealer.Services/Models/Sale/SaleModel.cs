namespace CarDealer.Services.Models.Sale
{
    using Car;

    public class SaleModel
    {
        public int Id { get; set; }

        public CarModel Car { get; set; }

        public string CustomerName { get; set; }

        public decimal Discount { get; set; }
    }
}
