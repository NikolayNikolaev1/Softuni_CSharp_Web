namespace CarDealer.Services.Contracts
{
    using Services.Models.Enums;
    using Services.Models.Supplier;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        ICollection<SupplierModel> All(SupplierType type);
    }
}
