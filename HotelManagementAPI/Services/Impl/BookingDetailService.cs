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
using HotelManagementAPI.Repositories.Impl;

namespace HotelManagementAPI.Services.Impl
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepo _bookingDetailRepo;
        public BookingDetailService(IBookingDetailRepo bookingDetailRepo)
        {
            _bookingDetailRepo = bookingDetailRepo;
        }

        public async Task<BaseResponse<BookingDetail>> Create(BookingDetail obj, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<BookingDetail>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<BookingDetail>.CreateMessage(brCode, "Chi tiết phiếu đặt")
            };
        }

        public async Task<BaseResponse<BookingDetail>> Update(BookingDetail obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<BookingDetail>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<BookingDetail>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Chi tiết phiếu đặt")
                };
            }

            var fnd = await _bookingDetailRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<BookingDetail>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<BookingDetail>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Chi tiết phiếu đặt")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _bookingDetailRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<BookingDetail>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<BookingDetail>.CreateMessage(brCode, "Chi tiết phiếu đặt")
            };
        }

        public async Task<BaseResponse<BookingDetail>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<BookingDetail>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<BookingDetail>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Chi tiết phiếu đặt")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _bookingDetailRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<BookingDetail>
            {
                Code = brCode,
                Message = BaseResponse<BookingDetail>.CreateMessage(brCode, "Chi tiết phiếu đặt")
            };
        }

        public async Task<BaseResponse<BookingDetail>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _bookingDetailRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<BookingDetail>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<BookingDetail>.CreateMessage(brCode, "Chi tiết phiếu đặt")
            };
        }

        public async Task<BaseResponse<List<BookingDetail>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _bookingDetailRepo.GetList(cancellationToken);
            return new BaseResponse<List<BookingDetail>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

        public async Task<BaseResponse<List<BookingDetail>>> GetByBookingId(string id, CancellationToken cancellationToken)
        {
            var results = await _bookingDetailRepo.GetList(cancellationToken);
            return new BaseResponse<List<BookingDetail>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results.Where(b => b.BookingId == id).ToList(),
            };
        }

    }
}
