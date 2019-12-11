using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IMakeService
    {
        Task AddAsync(IMake vehicleMake);
        Task UpdateAsync(IMake vehicleMake);
        Task RemoveAsync(IMake vehicleMake);
        Task<IEnumerable<IMake>> GetAllAsync();
        Task<IMake> GetByIdAsync(int id);
        //Task<IList<IMake>> ToPagedList(IPageModel pageModel);
    }
}
