using Project.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IModelRepository : IRepositoryBase<IModelEntity>
    {
        Task AddAsync(IModelEntity vehicleModel);
        Task UpdateAsync(IModelEntity vehicleModel);
        Task RemoveAsync(IModelEntity vehicleModel);
        Task<IList<IModelEntity>> GetAllByMakeIdAsync(int id);
        Task<IModelEntity> GetByIdAsync(int id);
    }
}
