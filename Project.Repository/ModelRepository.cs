using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Entities;
using Project.Repository.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class ModelRepository : RepositoryBase<IModelEntity>, IModelRepository
    {
        public ModelRepository(IVehicleContext context)
            : base(context)
        {
        }

        //This method gets and lists all vehicle models of a make with the same id from the database
        public async Task<IEnumerable<IModelEntity>> GetAllByMakeIdAsync(int id)
        {
            return await _context.Models.Where(m => m.MakeId == id).ToListAsync();
        }

        //This method gets a particlar make model and is found by its Id
        public async Task<IModelEntity> GetByIdAsync(int id)
        {
            return await _context.Models.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}
