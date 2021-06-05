namespace ByTheCake.Providers.Contracts
{
    using Models;

    public interface IOrderRepository : IRepository<Order>
    {
        decimal GetOrderTotalPrice(int id);
    }
}
