namespace CarDealer.Services
{
    using Contracts;
    using Data;
    using Models.Enums;
    using Models.Supplier;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext dbContext;

        public SupplierService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<SupplierModel> All(SupplierType type)
        {
            var suppliers = this.dbContext.Suppliers.AsQueryable();

            switch (type)
            {
                case SupplierType.Local:
                    suppliers = suppliers
                        .Where(s => !s.IsImporter);
                    break;
                case SupplierType.Importer:
                    suppliers = suppliers
                        .Where(s => s.IsImporter);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid supplier type: {type}");
            }

            return suppliers
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                }).ToList();
        }

        public ICollection<SupplierModel> All()
            => this.dbContext
            .Suppliers
            .Select(s => new SupplierModel
            {
                Id = s.Id,
                Name = s.Name,
                PartsCount = s.Parts.Count
            }).ToList();
    }
}
