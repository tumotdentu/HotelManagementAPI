using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;

namespace HotelManagementAPI.Services
{
    public interface IAuthService
    {
        Task<BaseResponse<LoginRes>> Login(LoginReq req, CancellationToken cancellationToken);
    }
}
