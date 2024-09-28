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
using HotelManagementAPI.Shared.Utils;

namespace HotelManagementAPI.Repositories.Impl
{
    public class AuthRepo : IAuthRepo
    {
        private readonly IConfiguration _configuration;
        private readonly HotelManagementContext _context;
        public AuthRepo(IConfiguration configuration, HotelManagementContext context)
        {
            this._configuration = configuration;
            _context = context;
        }

        public async Task<Manager> GetUserByUserNameAndPassword(LoginReq req, CancellationToken cancellationToken)
        {
            return await _context.Managers.SingleOrDefaultAsync(x => x.Email == req.UserName);
        }
    }
}
