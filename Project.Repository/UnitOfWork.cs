using Project.DAL;
using Project.Repository.Common;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IVehicleContext _context;
        private IMakeRepository _makeRepository;
        private IModelRepository _modelRepository;

        public UnitOfWork(IVehicleContext context)
        {
            _context = context;
        }

        public IMakeRepository Makes => _makeRepository = _makeRepository ?? new MakeRepository(_context);
        public IModelRepository Models => _modelRepository = _modelRepository ?? new ModelRepository(_context);

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}