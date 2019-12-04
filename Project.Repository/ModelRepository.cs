using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Entities;
using Project.Repository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class ModelRepository : RepositoryBase<IModelEntity>, IModelRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelRepository(IVehicleContext context, IUnitOfWork unitOfWork)
            : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(IModelEntity vehicleModel)
        {
            Add(vehicleModel);
            await _unitOfWork.Complete();
        }

        public async Task<IList<IModelEntity>> GetAllByMakeIdAsync(int id)
        {
            return await FindById(m => m.MakeId.Equals(id)).ToListAsync();
        }

        public async Task<IModelEntity> GetByIdAsync(int id)
        {
            return await FindById(m => m.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(IModelEntity vehicleModel)
        {
            Remove(vehicleModel);
            await _unitOfWork.Complete();
        }

        public async Task UpdateAsync(IModelEntity vehicleModel)
        {
            Update(vehicleModel);
            await _unitOfWork.Complete();
        }
    }
}
