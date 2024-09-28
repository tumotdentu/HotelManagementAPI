using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelManagementAPI.Datas.ViewModels.Base;

namespace HotelManagementAPI.Shared.Services
{
    public interface INewBaseService<T, ID>
    {        
        Task<BaseResponse<T>> Create(T obj, CancellationToken cancellationToken);
        Task<BaseResponse<T>> Update(T obj , CancellationToken cancellationToken);
        Task<BaseResponse<T>> Delete(ID id , CancellationToken cancellationToken);
        Task<BaseResponse<T>> GetById(ID id, CancellationToken cancellationToken );
        Task<BaseResponse<List<T>>> GetList(string searchKey, CancellationToken cancellationToken);
    }
}
