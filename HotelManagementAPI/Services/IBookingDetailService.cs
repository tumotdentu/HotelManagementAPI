using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Services;

namespace HotelManagementAPI.Services
{
    public interface IBookingDetailService : IBaseService<BookingDetail,string>
    {
        Task<BaseResponse<List<BookingDetail>>> GetByBookingId(string id, CancellationToken cancellationToken);
    }
}
