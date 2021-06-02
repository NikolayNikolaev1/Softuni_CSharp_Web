namespace ByTheCake.Providers
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> entities;

        public Repository(ByTheCakeDbContext context)
        {
            entities = context.Set<T>();
        }

        public void Add(T entity)
            => entities.Add(entity);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            => entities.Where(predicate);

        public void Remove(T entity)
            => entities.Remove(entity);
    }
}
