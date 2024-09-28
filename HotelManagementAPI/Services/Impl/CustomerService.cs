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
using HotelManagementAPI.Shared.Utils;
using Microsoft.Extensions.Configuration;
using HotelManagementAPI.Helpers;
using HotelManagementAPI.Utils;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace HotelManagementAPI.Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepo _customerRepo;
        public CustomerService(IConfiguration configuration, ICustomerRepo customerRepo)
        {
            this._configuration = configuration;
            _customerRepo = customerRepo;
        }
        public async Task<BaseResponse<LoginRes>> Login(LoginReq req, CancellationToken cancellationToken)
        {
            var user = await _customerRepo.GetUserByUserNameAndPassword(req, cancellationToken);
            if (user == null)
            {
                return new BaseResponse<LoginRes>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = string.Format(ResStatusConst.Message.NOT_FOUND, $"Tài khoản {req.UserName}"),
                };
            }
            var x = req.Password.ToMD5();
            if (user.Password != req.Password.ToMD5())
            {
                return new BaseResponse<LoginRes>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Sai mật khẩu"
                };
            }
            string token = GenerateToken(user, cancellationToken);
            return new BaseResponse<LoginRes>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = new LoginRes(token, user)
            };
        }
        private string GenerateToken(Customer user, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var nowUtc = DateTime.UtcNow;
            var expirationDuration =
                TimeSpan.FromMinutes(60); // Adjust as needed
            var expirationUtc = nowUtc.Add(expirationDuration);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub,
                               _configuration["JWT:Subject"]),
                     new Claim(JwtRegisteredClaimNames.Jti,
                               Guid.NewGuid().ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat,
                               EpochTime.GetIntDate(nowUtc).ToString(),
                               ClaimValueTypes.Integer64),
                     new Claim(JwtRegisteredClaimNames.Exp,
                               EpochTime.GetIntDate(expirationUtc).ToString(),
                               ClaimValueTypes.Integer64),
                     new Claim(JwtRegisteredClaimNames.Iss,
                               _configuration["JWT:Issuer"]),
                     new Claim(JwtRegisteredClaimNames.Aud,
                               _configuration["JWT:Audience"]),
                     new Claim("id", user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.Email ?? "Auth"),
                     new Claim(ClaimTypes.Role, user.Role ?? "Customer")

                }),
                Expires = expirationUtc,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<BaseResponse<Customer>> Register(Customer obj, CancellationToken cancellationToken)
        {
            var cuss = await this._customerRepo.GetList(cancellationToken);
            if(cuss.Any(c => c.Phone == obj.Phone || c.Email == obj.Email))
            {
                return new BaseResponse<Customer>
                {
                    Data = null,
                    Code = ResStatusConst.Code.ALREADY_EXISTS,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.ALREADY_EXISTS, "Khách hàng")
                };
            }
            obj.Password = obj.Password?.ToMD5();
            var result = await _customerRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Khách hàng")
            };
        }
        public async Task<BaseResponse<Customer>> Profile(string id, CancellationToken cancellationToken)
        {
            var result = await _customerRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Thông tin cá nhân")
            };
        }
        public async Task<BaseResponse<bool>> ChangePassword(string id, ChangePassword obj, CancellationToken cancellationToken)
        {
            if(obj.OldPassword == obj.NewPassword)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Mật khẩu cũ không được trùng với mật khẩu mới"
                };
            }    
            else if(obj.NewPassword != obj.ConfirmPassword)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = "Mật khẩu xác nhận không đúng"
                };
            }
            var fnd = await _customerRepo.GetById(id, cancellationToken);
            if(fnd == null) 
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
                if(fnd.Password != obj.OldPassword?.ToMD5()) 
                {
                    return new BaseResponse<bool>
                    {
                        Data = false,
                        Code = ResStatusConst.Code.INVALID_PARAM,
                        Message = "Mật khẩu cũ không chính xác"
                    };
                }

                fnd.Password = obj.NewPassword?.ToMD5();
                var result = await _customerRepo.Update(fnd, cancellationToken);
                int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
                return new BaseResponse<bool>
                {
                    Data = true,
                    Code = brCode,
                    Message = "Thay đổi mật khẩu thành công"
                };
            }
        }
        public async Task<BaseResponse<Customer>> Create(Customer obj, CancellationToken cancellationToken)
        {
            var cuss = await this._customerRepo.GetList(cancellationToken);
            if (cuss.Any(c => c.Phone == obj.Phone || c.Email == obj.Email))
            {
                return new BaseResponse<Customer>
                {
                    Data = null,
                    Code = ResStatusConst.Code.ALREADY_EXISTS,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.ALREADY_EXISTS, "Khách hàng")
                };
            }
            obj.Password = obj.Password?.ToMD5();
            var result = await _customerRepo.Insert(obj, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.SYSTEM_ERROR : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Khách hàng")
            };
        }

        public async Task<BaseResponse<Customer>> Update(Customer obj, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(obj.Id) != true)
            {
                return new BaseResponse<Customer>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Khách hàng")
                };
            }

            var fnd = await _customerRepo.GetById(obj.Id, cancellationToken);
            if (fnd == null)
            {
                return new BaseResponse<Customer>
                {
                    Code = ResStatusConst.Code.NOT_FOUND,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.NOT_FOUND, "Khách hàng")
                };
            }
            obj.Password = null;
            fnd = AutoMapperCF.MapperNotNull(obj, fnd);

            var result = await _customerRepo.Update(fnd, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Khách hàng")
            };
        }

        public async Task<BaseResponse<Customer>> Delete(string id, CancellationToken cancellationToken)
        {
            if (StringExtension.CheckGuid(id) != true)
            {
                return new BaseResponse<Customer>
                {
                    Code = ResStatusConst.Code.INVALID_PARAM,
                    Message = BaseResponse<Customer>.CreateMessage(ResStatusConst.Code.INVALID_PARAM, "Khách hàng")
                };
            }
            string delId = string.Empty;
            if (id != null)
            {
                delId = id;
            }
            int deleted = await _customerRepo.Delete(delId, cancellationToken);
            int brCode = (deleted == 0) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Khách hàng")
            };
        }

        public async Task<BaseResponse<Customer>> GetById(string id, CancellationToken cancellationToken)
        {
            var result = await _customerRepo.GetById(id, cancellationToken);
            int brCode = (result == null) ? ResStatusConst.Code.NOT_FOUND : ResStatusConst.Code.SUCCESS;
            return new BaseResponse<Customer>
            {
                Data = result,
                Code = brCode,
                Message = BaseResponse<Customer>.CreateMessage(brCode, "Khách hàng")
            };
        }

        public async Task<BaseResponse<List<Customer>>> GetList(CancellationToken cancellationToken)
        {
            var results = await _customerRepo.GetList(cancellationToken);
            return new BaseResponse<List<Customer>>
            {
                Code = ResStatusConst.Code.SUCCESS,
                Message = ResStatusConst.Message.SUCCESS,
                Data = results,
            };
        }

    }
}
