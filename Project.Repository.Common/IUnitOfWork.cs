using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
    }
}
