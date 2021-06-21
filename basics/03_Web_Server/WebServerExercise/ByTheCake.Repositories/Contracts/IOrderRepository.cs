namespace ByTheCake.Providers.Contracts
{
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;

    public interface IOrderRepository : IRepository<Order>
    {
        OrderDetailsViewModel Details(int orderId);

        IEnumerable<OrderListingViewModel> GetAll(int userId);

        decimal GetOrderTotalPrice(int id);
    }
}
