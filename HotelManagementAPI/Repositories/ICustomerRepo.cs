using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Repositories;

namespace HotelManagementAPI.Repositories
{
    public interface ICustomerRepo : IBaseRepository<Customer,string>
    {
        Task<Customer> GetUserByUserNameAndPassword(LoginReq req, CancellationToken cancellationToken);
    }
}
