namespace CarDealer.Services.Contracts
{
    using Models.Car;
    using System.Collections.Generic;

    public interface ICarService
    {
        void Add(string make, string model, double travelledDistance);

        ICollection<CarModel> GetByMake(string make);

        CarPartsModel FindWithParts(int id);
    }
}
