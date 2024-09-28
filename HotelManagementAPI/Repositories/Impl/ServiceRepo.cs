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
using HotelManagementAPI.Shared.Repositories;

namespace HotelManagementAPI.Repositories.Impl
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly HotelManagementContext _context;
        public ServiceRepo(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<Service> Insert(Service obj, CancellationToken cancellationToken)
        {
            _context.Services.Add(obj);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<Service> Update(Service obj, CancellationToken cancellationToken)
        {
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return obj;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
                throw;
            }
        }

        public async Task<int> Delete(string id, CancellationToken cancellationToken)
        {
            var obj = await _context.Services.FindAsync(id);
            if (obj == null)
            {
                return 0;
            }
            _context.Services.Remove(obj);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Service> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Service>> GetList(CancellationToken cancellationToken)
        {
            return await _context.Services.ToListAsync();
        }
    }
}
