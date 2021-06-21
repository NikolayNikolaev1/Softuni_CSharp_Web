namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ByTheCakeDbContext dbContext)
            : base(dbContext) { }

        public OrderDetailsViewModel Details(int orderId)
            => this.context
            .Orders
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDetailsViewModel
            {
                Id = o.Id,
                CreatedOn = o.CreationDate,
                Products = o.Products.Select(p => p.Product),
                TotalPrice = o.Products.Sum(p => p.Product.Price * p.ProductCount)
            }).FirstOrDefault();

        public IEnumerable<OrderListingViewModel> GetAll(int userId)
            => this.context
            .Orders
            .Where(o => o.UserId == userId)
            .Select(o => new OrderListingViewModel
            {
                Id = o.Id,
                CreatedOn = o.CreationDate,
                TotalSum = o.Products.Sum(p => p.Product.Price * p.ProductCount)
            }).ToList()
            .OrderByDescending(o => o.CreatedOn);

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
