namespace ByTheCake.Providers.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IOrderRepository OrderRepository { get; }

        IProductRepository ProductRepository { get; }

        void Save();
    }
}
