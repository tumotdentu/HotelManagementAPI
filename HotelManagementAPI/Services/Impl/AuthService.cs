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

namespace HotelManagementAPI.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepo _authRepo;
        public AuthService(IConfiguration configuration, IAuthRepo authRepo)
        {
            this._configuration = configuration;
            _authRepo = authRepo;
        }
        public async Task<BaseResponse<LoginRes>> Login(LoginReq req, CancellationToken cancellationToken)
        {
            var user = await _authRepo.GetUserByUserNameAndPassword(req, cancellationToken);
            if(user == null)
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
        private string GenerateToken(Manager user, CancellationToken cancellationToken)
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
                     new Claim(ClaimTypes.Role, user.Role ?? "Admin")

                }),
                Expires = expirationUtc,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
