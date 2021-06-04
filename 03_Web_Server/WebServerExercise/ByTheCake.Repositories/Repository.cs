namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ByTheCakeDbContext context;
        protected readonly DbSet<T> entities;

        public Repository(ByTheCakeDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public void Add(T entity)
            => entities.Add(entity);

        public T Find(int id)
            => entities.Find(id);

        public void Remove(T entity)
            => entities.Remove(entity);
    }
}
