using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected IVehicleContext _context;

        public RepositoryBase(IVehicleContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            //return await _context.Set<T>().AsNoTracking().ToListAsync();
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> FindById(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
            .Where(expression).AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
