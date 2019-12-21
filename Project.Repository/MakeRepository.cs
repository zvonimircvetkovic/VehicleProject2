using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Entities;
using Project.Repository.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class MakeRepository : RepositoryBase<IMakeEntity>, IMakeRepository
    {
        public MakeRepository(IVehicleContext context)
            : base(context)
        {
        }

        //This method gets and lists all vehicle makes from the database
        public IQueryable<IMakeEntity> GetAllAsync()
        {
            //return await _context.Makes.Include(m => m.ModelEntities).ToListAsync();
            return _context.Makes;
        }

        //This method gets a particlar make and is found by its Id
        public async Task<IMakeEntity> GetByIdAsync(int id)
        {
            return await _context.Makes.Where(m => m.Id ==id).FirstOrDefaultAsync();
        }
    }
}