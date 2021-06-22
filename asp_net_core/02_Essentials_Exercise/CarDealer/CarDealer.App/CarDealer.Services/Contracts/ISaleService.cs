namespace CarDealer.Services.Contracts
{
    using Models.Sale;
    using System.Collections.Generic;

    public interface ISaleService
    {
        ICollection<SaleModel> All();

        ICollection<SaleModel> AllDiscounted();

        ICollection<SaleModel> AllDiscounted(decimal discount);

        SaleModel Find(int id);
    }
}
