using System;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Repository.Common
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindById(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
