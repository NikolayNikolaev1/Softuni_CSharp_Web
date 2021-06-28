using System.ComponentModel.DataAnnotations;

namespace CarDealer.Services.Models.Part
{
    public class PartModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity is 1")]
        [Required]
        public int Quantity { get; set; }

        public string SupplierName { get; set; }
    }
}
