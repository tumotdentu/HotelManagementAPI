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

namespace HotelManagementAPI.Services.Impl
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IBookingRepo _bookingRepo;
        public ReviewService(IReviewRepo reviewRepo, IBookingRepo bookingRepo)
        {
            _reviewRepo = reviewRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task<BaseResponse<Review>> Create(Review obj, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepo.GetList(cancellationToken);
            if(bookings.Any(b => b.CustomerId == obj.CustomerId && b.RoomId == obj.RoomId))
            {
                var result = await _reviewRepo.Insert(obj, cancellationToken);
                int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
                return new BaseResponse<Review>
                {
                    Data = result,
                    Code = brCode,
                    Message = BaseResponse<Review>.CreateMessage(brCode, "Đánh giá")
                };
            }
            else
            {
                return new BaseResponse<Review>
                {
                    Data = null,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Tài khoản người dùng không có quyền để đánh giá phòng này"
                };
            }
           
        }

        public async Task<BaseResponse<Review>> Update(Review obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Review>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Review>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Đánh giá")
                };
            }

            var fnd = await _reviewRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Review>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Review>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Đánh giá")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _reviewRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Review>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Review>.CreateMessage(brCode, "Đánh giá")
            };
        }

        public async Task<BaseResponse<Review>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Review>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Review>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Đánh giá")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _reviewRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Review>
            {
                Code = brCode,
                Message = BaseResponse<Review>.CreateMessage(brCode, "Đánh giá")
            };
        }

        public async Task<BaseResponse<Review>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _reviewRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Review>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Review>.CreateMessage(brCode, "Đánh giá")
            };
        }
        public async Task<BaseResponse<List<Review>>> GetByRoomId(string id, CancellationToken cancellationToken)
        {
            var result = await _reviewRepo.GetList(cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<List<Review>>
            {
                Data = result?.Where(b => b.RoomId == id).ToList(),
                Code = brCode,
                Message = BaseResponse<Review>.CreateMessage(brCode, "Đánh giá")
            };
        }
        public async Task<BaseResponse<List<Review>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _reviewRepo.GetList(cancellationToken);
            return new BaseResponse<List<Review>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
