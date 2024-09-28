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
    public class ManagerRepo : IManagerRepo
    {
        private readonly HotelManagementContext _context;
        public ManagerRepo(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<Manager> Insert(Manager obj, CancellationToken cancellationToken)
        {
            _context.Managers.Add(obj);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<Manager> Update(Manager obj, CancellationToken cancellationToken)
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
            var obj = await _context.Managers.FindAsync(id);
            if (obj == null)
            {
                return 0;
            }
            _context.Managers.Remove(obj);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Manager> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context.Managers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Manager>> GetList(CancellationToken cancellationToken)
        {
            return await _context.Managers.ToListAsync();
        }
    }
}
