namespace CarDealer.Services
{
    using Contracts;
    using Data;
    using Models.Car;
    using Models.Sale;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext dbContext;

        public SaleService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<SaleModel> All()
            => this.dbContext
            .Sales
            .Select(s => new SaleModel
            {
                Id = s.Id
            }).ToList();

        public ICollection<SaleModel> AllDiscounted()
            => this.dbContext
            .Sales
            .Where(s => s.Discount != 0)
            .Select(s => new SaleModel
            {
                Id = s.Id,
                Discount = s.Discount
            }).ToList();

        public ICollection<SaleModel> AllDiscounted(decimal discount)
            => this.dbContext
            .Sales
            .Where(s => s.Discount == discount)
            .Select(s => new SaleModel
            {
                Id = s.Id,
                Discount = s.Discount
            }).ToList();

        public SaleModel Find(int id)
            => this.dbContext
            .Sales
            .Where(s => s.Id == id)
            .Select(s => new SaleModel
            {
                Id = s.Id,
                Car = new CarModel
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TraveledDistance = s.Car.TravelledDistance
                },
                CustomerName = s.Customer.Name

            }).FirstOrDefault();
    }
}
