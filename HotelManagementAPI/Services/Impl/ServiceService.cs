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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepo _serviceRepo;
        public ServiceService(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<BaseResponse<Service>> Create(Service obj, CancellationToken cancellationToken)
        {
            var result = await _serviceRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Service>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Service>.CreateMessage(brCode, "Dịch vụ")
            };
        }

        public async Task<BaseResponse<Service>> Update(Service obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Service>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Service>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Dịch vụ")
                };
            }

            var fnd = await _serviceRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Service>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Service>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Dịch vụ")
                };
            }

            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _serviceRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Service>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Service>.CreateMessage(brCode, "Dịch vụ")
            };
        }

        public async Task<BaseResponse<Service>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Service>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Service>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Dịch vụ")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _serviceRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Service>
            {
                Code = brCode,
                Message = BaseResponse<Service>.CreateMessage(brCode, "Dịch vụ")
            };
        }

        public async Task<BaseResponse<Service>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _serviceRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Service>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Service>.CreateMessage(brCode, "Dịch vụ")
            };
        }

        public async Task<BaseResponse<List<Service>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _serviceRepo.GetList(cancellationToken);
            return new BaseResponse<List<Service>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
