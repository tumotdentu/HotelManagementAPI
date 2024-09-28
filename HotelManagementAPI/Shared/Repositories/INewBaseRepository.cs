using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelManagementAPI.Shared.Repositories
{
    public interface INewBaseRepository<T, ID>
    {
        Task<T> Insert(T obj, CancellationToken cancellationToken);
        Task<T> Update(T obj, CancellationToken cancellationToken);
        Task<int> Delete(ID id, CancellationToken cancellationToken);
        Task<T> GetById(ID id, CancellationToken cancellationToken);
        Task<List<T>> GetList(string searchKey, CancellationToken cancellationToken);
    }
}
