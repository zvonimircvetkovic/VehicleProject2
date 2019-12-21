using Project.Common.Filter;
using Project.Model.Common;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IModelService
    {
        Task AddAsync(IModel vehicleModel);
        Task UpdateAsync(IModel vehicleModel);
        Task RemoveAsync(IModel vehicleModel);
        Task<PagedList<IModel>> GetAllByMakeIdAsync(int id, PageModel page, SearchModel search, SortModel sort);
        Task<IModel> GetByIdAsync(int id);
    }
}
