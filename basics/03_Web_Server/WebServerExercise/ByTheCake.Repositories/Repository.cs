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
        }

        public void Add(T entity)
            => this.context.Set<T>().Add(entity);

        public T Find(int id)
            => this.context.Set<T>().Find(id);

        public void Remove(T entity)
            => this.context.Set<T>().Remove(entity);
    }
}
