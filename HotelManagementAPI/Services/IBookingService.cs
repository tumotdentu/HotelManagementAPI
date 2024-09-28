using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Services;

namespace HotelManagementAPI.Services
{
    public interface IBookingService : IBaseService<Booking,string>
    {
        Task<BaseResponse<Booking>> Booking(Booking obj, CancellationToken cancellationToken);
        Task<BaseResponse<Booking>> Extend(ExtendModel obj, CancellationToken cancellationToken);
        Task<BaseResponse<List<Booking>>> GetByCustomerId(string id, CancellationToken cancellationToken);
    }
}
