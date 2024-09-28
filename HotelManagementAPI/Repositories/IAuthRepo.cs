using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;

namespace HotelManagementAPI.Repositories
{
    public interface IAuthRepo
    {
        Task<Manager> GetUserByUserNameAndPassword(LoginReq req, CancellationToken cancellationToken);
    }
}
