using Project.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IModelService
    {
        Task AddAsync(IModel vehicleModel);
        Task UpdateAsync(IModel vehicleModel);
        Task RemoveAsync(IModel vehicleModel);
        Task<IList<IModel>> GetAllByMakeIdAsync(int id);
        Task<IModel> GetByIdAsync(int id);
    }
}
