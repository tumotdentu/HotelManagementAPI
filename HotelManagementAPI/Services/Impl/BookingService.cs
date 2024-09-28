using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelManagementAPI.Datas.Models;
using HotelManagementAPI.Datas.ViewModels;
using HotelManagementAPI.Datas.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using HotelManagementAPI.Extension;
using HotelManagementAPI.Repositories;
using Microsoft.OpenApi.Extensions;
using HotelManagementAPI.Utils;
using HotelManagementAPI.Shared.Utils;
using HotelManagementAPI.Helpers;
using HotelManagementAPI.Shared.Constants;
using System.Runtime.Remoting;

namespace HotelManagementAPI.Services.Impl
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IRoomRepo _roomRepo;
        public BookingService(IBookingRepo bookingRepo, IRoomRepo roomRepo)
        {
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
        }
        public async Task<BaseResponse<Booking>> Booking(Booking obj, CancellationToken cancellationToken)
        {

            if (obj.CheckInDate < DateTime.Now)
            {
                return new BaseResponse<Booking>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Ngày đặt phòng phải lớn hơn ngày hiện tại."
                };
            }
            if (obj.CheckInDate > obj.CheckOutDate)
            {
                return new BaseResponse<Booking>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Ngày nhận phòng phải không được lớn hơn ngày trả phòng."
                };
            }
            if ((obj.CheckInDate - DateTime.Now)?.TotalDays > 10)
            {
                return new BaseResponse<Booking>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Ngày nhận phòng phải nhỏ hơn 10 ngày so với thời gian hiện tại."
                };
            }
            var bookings = await _bookingRepo.GetList(cancellationToken);
            var bookingByRoom = bookings.Where(b => b.RoomId == obj.RoomId && b.Status != StatusBooking.Cancelled && b.CheckInDate >= DateTime.Now);

            if (bookingByRoom.Any(b =>
                (b.IsExtend == false &&
                    ((b.CheckInDate <= obj.CheckInDate && b.CheckOutDate?.AddDays(3) >= obj.CheckInDate) ||
                     (b.CheckInDate <= obj.CheckOutDate?.AddDays(3) && b.CheckOutDate?.AddDays(3) >= obj.CheckOutDate))) ||
                (b.IsExtend == true &&
                    ((b.CheckInDate <= obj.CheckInDate && b.CheckOutDate >= obj.CheckInDate) ||
                     (b.CheckInDate <= obj.CheckOutDate?.AddDays(3) && b.CheckOutDate >= obj.CheckOutDate?.AddDays(3)))) ||
                (b.CheckInDate <= obj.CheckInDate && b.CheckOutDate >= obj.CheckOutDate) ||
                (b.CheckInDate <= obj.CheckOutDate?.AddDays(3) && b.CheckOutDate >= obj.CheckInDate)
            ))
            {
                return new BaseResponse<Booking>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Phòng không có sẵn trong khoảng thời gian này"
                };
            }
            obj.IsExtend = false;
            var result = await _bookingRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<Booking>> Extend(ExtendModel obj, CancellationToken cancellationToken)
        {
            if (obj.DayExtend != null && obj.DayExtend > 3)
            {
                return new BaseResponse<Booking>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Số ngày gia hạn không được quá 3 ngày."
                };
            }
            if (StringExtension.CheckGuid(obj.BookingId) != true || StringExtension.CheckGuid(obj.CustomerId) != true)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Booking>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Id")
                };
            }
            var booking = await this._bookingRepo.GetById(obj.BookingId, cancellationToken);
            if (booking == null)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Booking>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Phiếu đặt")
                };
            }
            if (booking.IsExtend == true)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Phiếu đặt đã được gia hạn trước đó"
                };
            }
            if (booking.CustomerId != obj.CustomerId)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Thông tin khách hàng không trùng khớp với khách hạng của phiếu đặt này"
                };
            }
            booking.CheckOutDate = booking.CheckOutDate?.AddDays(obj.DayExtend.Value);
            booking.IsExtend = true;
            var result = await _bookingRepo.Update(booking, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Data = result,
                Code = brCode,
                Message = brCode == ResStatusConst.Code.SUCCESS ? "Gia hạn thành công" : BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<Booking>> Create(Booking obj, CancellationToken cancellationToken)
        {
            var result = await _bookingRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<Booking>> Update(Booking obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Booking>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Phiếu đặt")
                };
            }

            var fnd = await _bookingRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Booking>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Phiếu đặt")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _bookingRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<Booking>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Booking>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Booking>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Phiếu đặt")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _bookingRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<Booking>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _bookingRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Booking>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }

        public async Task<BaseResponse<List<Booking>>> GetByCustomerId(string id, CancellationToken cancellationToken)
        {
            var result = await _bookingRepo.GetList(cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<List<Booking>>
            {
                Data = result?.Where(b => b.CustomerId == id).ToList(),
                Code = brCode,
                Message = BaseResponse<Booking>.CreateMessage(brCode, "Phiếu đặt")
            };
        }
        public async Task<BaseResponse<List<Booking>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _bookingRepo.GetList(cancellationToken);
            return new BaseResponse<List<Booking>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }


    }
}
