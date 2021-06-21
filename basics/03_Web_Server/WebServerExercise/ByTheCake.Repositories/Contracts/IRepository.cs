namespace ByTheCake.Providers.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T Find(int id);

        void Remove(T entity);
    }
}
