using Project.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IMakeRepository : IRepositoryBase<IMakeEntity>
    {
        IQueryable<IMakeEntity> GetAllAsync();
        Task<IMakeEntity> GetByIdAsync(int id);
    }
}
