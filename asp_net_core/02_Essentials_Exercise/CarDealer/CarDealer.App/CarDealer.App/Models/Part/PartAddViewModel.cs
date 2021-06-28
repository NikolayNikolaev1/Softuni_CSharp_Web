namespace CarDealer.App.Models.Part
{
    using Services.Models.Supplier;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartAddViewModel
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimum quantity is 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please choose a supplier")]
        public int SupplierId { get; set; }

        public ICollection<SupplierModel> Suppliers { get; set; }
    }
}
