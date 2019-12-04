using Project.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IMakeRepository : IRepositoryBase<IMakeEntity>
    {
        Task AddAsync(IMakeEntity vehicleMake);
        Task UpdateAsync(IMakeEntity vehicleMake);
        Task RemoveAsync(IMakeEntity vehicleMake);
        Task<IList<IMakeEntity>> GetAllAsync();
        Task<IMakeEntity> GetByIdAsync(int id);
    }
}
