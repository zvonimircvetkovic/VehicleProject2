using Project.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IModelRepository : IRepositoryBase<IModelEntity>
    {
        Task<IEnumerable<IModelEntity>> GetAllByMakeIdAsync(int id);
        Task<IModelEntity> GetByIdAsync(int id);
    }
}
