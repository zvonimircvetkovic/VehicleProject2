using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected IVehicleContext _context;

        public RepositoryBase(IVehicleContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindById(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
            .Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
