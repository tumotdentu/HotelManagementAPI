using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using HotelManagementAPI.Shared.Services;

namespace HotelManagementAPI.Services
{
    public interface IInvoiceService : IBaseService<Invoice,string>
    {
        Task<BaseResponse<List<Invoice>>> GetByCustomerId(string id, CancellationToken cancellationToken);
    }
}
