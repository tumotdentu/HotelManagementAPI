using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Services;

namespace HotelManagementAPI.Services
{
    public interface IManagerService : IBaseService<Manager,string>
    {
        Task<BaseResponse<Manager>> Profile(string id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> ChangePassword(string id, ChangePassword obj, CancellationToken cancellationToken);
    }
}
