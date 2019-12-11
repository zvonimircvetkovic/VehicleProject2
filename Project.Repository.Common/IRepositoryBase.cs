using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindById(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
