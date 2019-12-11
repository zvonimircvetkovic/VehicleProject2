using System;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IMakeRepository Makes { get; }
        IModelRepository Models { get; }

        Task<int> Complete();
    }
}
