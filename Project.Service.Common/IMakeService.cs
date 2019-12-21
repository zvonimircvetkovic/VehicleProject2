using Project.Common.Filter;
using Project.Model.Common;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IMakeService
    {
        Task AddAsync(IMake vehicleMake);
        Task UpdateAsync(IMake vehicleMake);
        Task RemoveAsync(IMake vehicleMake);
        Task<PagedList<IMake>> GetAllAsync(PageModel page, SearchModel search, SortModel sort);
        Task<IMake> GetByIdAsync(int id);
    }
}
