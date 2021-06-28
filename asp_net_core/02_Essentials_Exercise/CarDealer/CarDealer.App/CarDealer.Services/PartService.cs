namespace CarDealer.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Part;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext dbContext;

        public PartService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(string name, decimal price, int quantity, int supplierId)
        {
            this.dbContext
                .Parts.Add(new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId
            });

            this.dbContext.SaveChanges();
        }

        public ICollection<PartModel> All()
            => this.dbContext
            .Parts
            .Select(p => new PartModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                SupplierName = p.Supplier.Name

            }).ToList();

        public bool Delete(int id)
        {
            var part = this.dbContext
                .Parts
                .FirstOrDefault(p => p.Id == id);

            if (part == null)
            {
                return false;
            }

            this.dbContext
                .Parts
                .Remove(part);

            this.dbContext.SaveChanges();

            return true;
        }

        public bool Edit(int id, string name, int quantity)
        {
            var part = this.dbContext
                .Parts
                .FirstOrDefault(p => p.Id == id);

            if (part == null)
            {
                return false;
            }

            part.Name = name;
            part.Quantity = quantity;
            this.dbContext.SaveChanges();

            return true;
        }

        public PartModel Find(int id)
            => this.dbContext
            .Parts
            .Where(p => p.Id == id)
            .Select(p => new PartModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                SupplierName = p.Supplier.Name
            }).FirstOrDefault();
    }
}
