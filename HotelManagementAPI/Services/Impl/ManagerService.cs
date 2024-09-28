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
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepo _managerRepo;
        public ManagerService(IManagerRepo managerRepo)
        {
            _managerRepo = managerRepo;
        }
        public async Task<BaseResponse<Manager>> Profile(string id, CancellationToken cancellationToken)
        {
            var result = await _managerRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Manager>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Manager>.CreateMessage(brCode, "Thông tin cá nhân")
            };
        }
        public async Task<BaseResponse<bool>> ChangePassword(string id, ChangePassword obj, CancellationToken cancellationToken)
        {
            if (obj.OldPassword == obj.NewPassword)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Mật khẩu cũ không được trùng với mật khẩu mới"
                };
            }
            else if (obj.NewPassword != obj.ConfirmPassword)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Mật khẩu xác nhận không đúng"
                };
            }
            var fnd = await _managerRepo.GetById(id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = "Không tìm thấy thông tin tài khoản"
                };
            }
            else
            {
                if (fnd.Password != obj.OldPassword?.ToMD5())
                {
                    return new BaseResponse<bool>
                    {
                        Data = false,
                        Code = ResStatusConst.Code.INVALID_PARAM,
                        Message = "Mật khẩu cũ không chính xác"
                    };
                }

                fnd.Password = obj.NewPassword?.ToMD5();
                var result = await _managerRepo.Update(fnd, cancellationToken);
                int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
                return new BaseResponse<bool>
                {
                    Data = true,
                    Code = brCode,
                    Message = "Thay đổi mật khẩu thành công"
                };
            }
        }
        public async Task<BaseResponse<Manager>> Create(Manager obj, CancellationToken cancellationToken)
        {
            var cuss = await this._managerRepo.GetList(cancellationToken);
            if (cuss.Any(c => c.Phone == obj.Phone || c.Email == obj.Email))
            {
                return new BaseResponse<Manager>
                {
                    Data = null,
                    Code = ResStatusConst.Code.ALREADY_EXISTS,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.ALREADY_EXISTS, "Quản lý")
                };
            }
            obj.Password = obj.Password?.ToMD5();
            var result = await _managerRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Manager>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Manager>.CreateMessage(brCode, "Quản lý")
            };
        }

        public async Task<BaseResponse<Manager>> Update(Manager obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Manager>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Manager>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Quản lý")
                };
            }

            var fnd = await _managerRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Manager>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Manager>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Quản lý")
                };
            }
            obj.Password = null;
            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _managerRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Manager>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Manager>.CreateMessage(brCode, "Quản lý")
            };
        }

        public async Task<BaseResponse<Manager>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Manager>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Manager>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Quản lý")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _managerRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Manager>
            {
                Code = brCode,
                Message = BaseResponse<Manager>.CreateMessage(brCode, "Quản lý")
            };
        }

        public async Task<BaseResponse<Manager>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _managerRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Manager>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Manager>.CreateMessage(brCode, "Quản lý")
            };
        }

        public async Task<BaseResponse<List<Manager>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _managerRepo.GetList(cancellationToken);
            return new BaseResponse<List<Manager>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
