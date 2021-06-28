namespace CarDealer.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Part;
    using Services.Contracts;
    using Services.Models.Part;

    public class PartController : Controller
    {
        private readonly ISupplierService suppliers;
        private readonly IPartService parts;

        public PartController(ISupplierService suppliers, IPartService parts)
        {
            this.suppliers = suppliers;
            this.parts = parts;
        }

        public IActionResult Add()
            => View(new PartAddViewModel
            {
                Suppliers = this.suppliers.All()
            });

        [HttpPost]
        public IActionResult Add(PartAddViewModel model)
        {
            model.Suppliers = this.suppliers.All();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string name = model.Name;
            decimal price = model.Price;
            int supplierId = model.SupplierId;
            int quantity = model.Quantity;

            if (quantity == 0)
            {
                quantity = 1;
            }

            this.parts.Add(name, price, quantity, supplierId);

            return Redirect("/");
        }

        public IActionResult All()
            => View(this.parts.All());


        public IActionResult Delete(int id)
        {
            bool result = this.parts.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Redirect("/part/all");
        }

        public IActionResult Edit(int id)
        {
            PartModel model = this.parts.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, PartModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = this.parts.Edit(id, model.Name, model.Quantity);

            if (!result)
            {
                return BadRequest();
            }

            return Redirect("/part/all");
        }
    }
}
