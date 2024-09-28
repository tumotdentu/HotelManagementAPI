using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Services;

namespace HotelManagementAPI.Services
{
    public interface ICustomerService : IBaseService<Customer,string>
    {
        Task<BaseResponse<Customer>> Register(Customer customer, CancellationToken cancellationToken);
        Task<BaseResponse<LoginRes>> Login(LoginReq req, CancellationToken cancellationToken);
        Task<BaseResponse<Customer>> Profile(string id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> ChangePassword(string id, ChangePassword obj, CancellationToken cancellationToken);
    }
}
