using Project.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IModelRepository : IRepositoryBase<IModelEntity>
    {
        IQueryable<IModelEntity> GetAllByMakeIdAsync(int id);
        Task<IModelEntity> GetByIdAsync(int id);
    }
}
