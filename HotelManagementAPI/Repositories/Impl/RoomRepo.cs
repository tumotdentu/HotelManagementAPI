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
using HotelManagementAPI.Utils;

namespace HotelManagementAPI.Repositories.Impl
{
    public class RoomRepo : IRoomRepo
    {
        private readonly HotelManagementContext _context;
        public RoomRepo(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<Room> Insert(Room obj, CancellationToken cancellationToken)
        {
            _context.Rooms.Add(obj);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<Room> Update(Room obj, CancellationToken cancellationToken)
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
            var obj = await _context.Rooms.FindAsync(id);
            if (obj == null)
            {
                return 0;
            }
            _context.Rooms.Remove(obj);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Room> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context.Rooms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Room>> GetList(string searchKey, CancellationToken cancellationToken)
        {

            var rooms = await _context.Rooms.ToListAsync();
            if(!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = StringNormalizationExtensions.Normalize(searchKey);
                rooms = rooms.Where(r => StringExtension.IsSubstring(StringNormalizationExtensions.Normalize(r.Name ?? ""), searchKey) ||
                StringExtension.IsSubstring(StringNormalizationExtensions.Normalize(r.Type ?? ""), searchKey) ||
                StringExtension.IsSubstring(StringNormalizationExtensions.Normalize(r.Description ?? ""), searchKey)
                ).ToList();
            }  
            return rooms;
        }
    }
}
