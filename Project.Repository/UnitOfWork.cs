using Project.DAL;
using Project.Repository.Common;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IVehicleContext _context;

        public UnitOfWork(IVehicleContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}