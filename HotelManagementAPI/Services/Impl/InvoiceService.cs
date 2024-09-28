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
    public class InvoiceService : IInvoiceService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IInvoiceRepo _invoiceRepo;
        public InvoiceService(IInvoiceRepo invoiceRepo, IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
            _invoiceRepo = invoiceRepo;
        }

        public async Task<BaseResponse<Invoice>> Create(Invoice obj, CancellationToken cancellationToken)
        {
            var result = await _invoiceRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Invoice>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Invoice>.CreateMessage(brCode, "Hóa đơn")
            };
        }

        public async Task<BaseResponse<Invoice>> Update(Invoice obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Invoice>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Invoice>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Hóa đơn")
                };
            }

            var fnd = await _invoiceRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Invoice>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Invoice>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Hóa đơn")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _invoiceRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Invoice>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Invoice>.CreateMessage(brCode, "Hóa đơn")
            };
        }

        public async Task<BaseResponse<Invoice>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Invoice>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Invoice>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Hóa đơn")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _invoiceRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Invoice>
            {
                Code = brCode,
                Message = BaseResponse<Invoice>.CreateMessage(brCode, "Hóa đơn")
            };
        }

        public async Task<BaseResponse<Invoice>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _invoiceRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Invoice>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Invoice>.CreateMessage(brCode, "Hóa đơn")
            };
        }
        public async Task<Invoice> GetByBookingId(string id, CancellationToken cancellationToken)
        {
            var result = await _invoiceRepo.GetList(cancellationToken);
            return result != null ? result.FirstOrDefault(i => i.BookingId == id) : null;
        }
        public async Task<BaseResponse<List<Invoice>>> GetByCustomerId(string id, CancellationToken cancellationToken)
        {
            var result = await _bookingRepo.GetList(cancellationToken);
            
            if (result == null)
            {
                return new BaseResponse<List<Invoice>>
                {
                    Data = new List<Invoice>(),
                    Code = ResStatusConst.Code.SUCCESS,
                    Message = ResStatusConst.Message.SUCCESS,
                };
            }
            var bookings = result.Where(b => b.CustomerId == id).ToList();
            List<Invoice> listInvs = new List<Invoice>();
            foreach (var item in bookings)
            {
                var invoice = await GetByBookingId(item.Id, cancellationToken);
                listInvs.Add(invoice);
            }
            return new BaseResponse<List<Invoice>>
            {
                Data = listInvs,
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
            };
        }
        public async Task<BaseResponse<List<Invoice>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _invoiceRepo.GetList(cancellationToken);
            return new BaseResponse<List<Invoice>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
