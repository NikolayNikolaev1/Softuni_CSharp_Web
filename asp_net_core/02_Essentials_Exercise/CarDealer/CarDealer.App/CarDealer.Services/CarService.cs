namespace CarDealer.Services
{
    using Data;
    using Models.Part;
    using Services.Contracts;
    using Services.Models.Car;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext dbContext;

        public CarService(CarDealerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CarPartsModel FindWithParts(int id)
            => this.dbContext
            .Cars
            .Where(c => c.Id == id)
            .Select(c => new CarPartsModel
            {
                Car = this.dbContext
                .Cars
                .Where(c => c.Id == id)
                .Select(car => new CarModel
                {
                    Make = car.Make,
                    Model = car.Model,
                    TraveledDistance = car.TravelledDistance
                }).FirstOrDefault(),
                Parts = c.Parts.Select(p => new PartModel
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price
                }).ToList()
            }).FirstOrDefault();

        public ICollection<CarModel> GetByMake(string make)
            => this.dbContext
            .Cars
            .Where(c => c.Make.ToLower().Equals(make))
            .Select(c => new CarModel
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TravelledDistance
            }).OrderBy(c => c.Model)
            .ThenByDescending(c => c.TraveledDistance)
            .ToList();
    }
}
