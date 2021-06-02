namespace ByTheCake.Providers.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Remove(T entity);
    }
}
