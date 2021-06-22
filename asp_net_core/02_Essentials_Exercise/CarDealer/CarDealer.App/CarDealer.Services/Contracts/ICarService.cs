namespace CarDealer.Services.Contracts
{
    using Models.Car;
    using System.Collections.Generic;

    public interface ICarService
    {
        ICollection<CarModel> GetByMake(string make);

        CarPartsModel FindWithParts(int id);
    }
}
