using System.Threading;
using System.Threading.Tasks;

namespace Mog.Api.Core.Abstractions
{
    public interface IStore<T> : IStoreBase
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = new CancellationToken());

        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = new CancellationToken());
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = new CancellationToken());
    }
}