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
    public class BookingDetailRepo : IBookingDetailRepo
    {
        private readonly HotelManagementContext _context;
        public BookingDetailRepo(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<BookingDetail> Insert(BookingDetail obj, CancellationToken cancellationToken)
        {
            _context.BookingDetails.Add(obj);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<BookingDetail> Update(BookingDetail obj, CancellationToken cancellationToken)
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
            var obj = await _context.BookingDetails.FindAsync(id);
            if (obj == null)
            {
                return 0;
            }
            _context.BookingDetails.Remove(obj);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<BookingDetail> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context.BookingDetails.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<BookingDetail>> GetList(CancellationToken cancellationToken)
        {
            return await _context.BookingDetails.ToListAsync();
        }
    }
}
