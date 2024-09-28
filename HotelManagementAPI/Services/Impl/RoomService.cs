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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepo _roomRepo;
        public RoomService(IRoomRepo roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public async Task<BaseResponse<Room>> Create(Room obj, CancellationToken cancellationToken)
        {
            var result = await _roomRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Room>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Room>.CreateMessage(brCode, "Phòng")
            };
        }

        public async Task<BaseResponse<Room>> Update(Room obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Room>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Room>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Phòng")
                };
            }

            var fnd = await _roomRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Room>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Room>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Phòng")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _roomRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Room>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Room>.CreateMessage(brCode, "Phòng")
            };
        }

        public async Task<BaseResponse<Room>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Room>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Room>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Phòng")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _roomRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Room>
            {
                Code = brCode,
                Message = BaseResponse<Room>.CreateMessage(brCode, "Phòng")
            };
        }

        public async Task<BaseResponse<Room>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _roomRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Room>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Room>.CreateMessage(brCode, "Phòng")
            };
        }

        public async Task<BaseResponse<List<Room>>> GetList(string searchKey, CancellationToken cancellationToken)
        {
            var results = await _roomRepo.GetList(searchKey, cancellationToken);
            return new BaseResponse<List<Room>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
