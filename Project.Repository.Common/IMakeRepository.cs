using Project.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IMakeRepository : IRepositoryBase<IMakeEntity>
    {
        Task<IEnumerable<IMakeEntity>> GetAllAsync();
        Task<IMakeEntity> GetByIdAsync(int id);
    }
}
