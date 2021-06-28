namespace CarDealer.Services.Contracts
{
    using Models.Part;
    using System.Collections.Generic;

    public interface IPartService
    {
        void Add(string name, decimal price, int quantity, int supplierId);

        ICollection<PartModel> All();

        bool Delete(int id);

        bool Edit(int id, string name, int quantity);

        PartModel Find(int id);
    }
}
