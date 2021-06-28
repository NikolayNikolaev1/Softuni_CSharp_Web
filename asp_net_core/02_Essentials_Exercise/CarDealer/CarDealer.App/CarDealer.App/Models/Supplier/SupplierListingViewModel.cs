namespace CarDealer.App.Models.Supplier
{
    using Services.Models.Enums;
    using Services.Models.Supplier;
    using System.Collections.Generic;

    public class SupplierListingViewModel
    {
        public ICollection<SupplierModel> Suppliers { get; set; }

        public SupplierType? SupplierType { get; set; }
    }
}
