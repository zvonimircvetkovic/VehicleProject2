using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Entities;
using Project.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class MakeRepository : RepositoryBase<IMakeEntity>, IMakeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MakeRepository(IVehicleContext context, IUnitOfWork unitOfWork)
            : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        //This method adds a new vehicle make to the DB
        public async Task AddAsync(IMakeEntity vehicleMake)
        {
            Add(vehicleMake);
            await _unitOfWork.Complete();
        }

        //This method updates an existing make in the DB
        public async Task UpdateAsync(IMakeEntity vehicleMake)
        {
            Update(vehicleMake);
            await _unitOfWork.Complete();
        }

        //This method removes an existing make from the database
        public async Task RemoveAsync(IMakeEntity vehicleMake)
        {
            Remove(vehicleMake);
            await _unitOfWork.Complete();
        }

        //This method gets and lists all vehicle makes from the database
        public async Task<IList<IMakeEntity>> GetAllAsync()
        {
            return await _context.Set<IMakeEntity>().Include(m => m.ModelEntities).ToListAsync();
        }

        //This method gets a particlar make and is found by its Id
        public async Task<IMakeEntity> GetByIdAsync(int id)
        {
            return await FindById(m => m.Id.Equals(id)).FirstOrDefaultAsync();
        }
    }
}