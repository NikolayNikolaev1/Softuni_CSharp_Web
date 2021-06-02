namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Models;

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<User> userRepo;
        private IRepository<Order> orderRepo;
        private IRepository<Product> productRepo;
        private ByTheCakeDbContext dbContext;

        public UnitOfWork(ByTheCakeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return new UserRepository(this.dbContext);
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                return new OrderRepository(this.dbContext);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return new ProductRepository(this.dbContext);
            }
        }

        public void Save()
            => this.dbContext.SaveChanges();
    }
}
