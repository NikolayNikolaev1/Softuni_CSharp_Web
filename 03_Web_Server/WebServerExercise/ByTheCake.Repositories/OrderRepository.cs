namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;
    using System.Linq;

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public decimal GetOrderTotalPrice(int id)
        {
            decimal totalPrice = 0;
            Order order = base.Find(id);

            foreach (OrderProduct orderProduct in order.Products)
            {
                totalPrice += orderProduct.Product.Price;
            }

            return totalPrice;
        }
    }
}
