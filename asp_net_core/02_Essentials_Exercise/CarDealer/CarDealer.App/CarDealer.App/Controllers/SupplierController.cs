namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Supplier;
    using Services.Contracts;
    using Services.Models.Enums;

    public class SupplierController : Controller
    {
        private readonly ISupplierService suppliers;

        public SupplierController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        [Route("/suppliers/{type}")]
        public IActionResult All(string type)
        {
            SupplierType supplierType;

            if (type.ToLower() == "local")
            {
                supplierType = SupplierType.Local;
            }
            else if (type.ToLower() == "importer")
            {
                supplierType = SupplierType.Importer;
            }
            else
            {
                return NotFound();
            }

            return View(new SupplierListingViewModel
            {
                Suppliers = this.suppliers.All(supplierType),
                SupplierType = supplierType
            });
        }
    }
}
