namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }
    }
}
