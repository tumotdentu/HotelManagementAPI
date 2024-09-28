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
    public class ReviewRepo : IReviewRepo
    {
        private readonly HotelManagementContext _context;
        public ReviewRepo(HotelManagementContext context)
        {
            _context = context;
        }

        public async Task<Review> Insert(Review obj, CancellationToken cancellationToken)
        {
            _context.Reviews.Add(obj);
            await _context.SaveChangesAsync(cancellationToken);
            return obj;
        }

        public async Task<Review> Update(Review obj, CancellationToken cancellationToken)
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
            var obj = await _context.Reviews.FindAsync(id);
            if (obj == null)
            {
                return 0;
            }
            _context.Reviews.Remove(obj);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Review> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context.Reviews.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Review>> GetList(CancellationToken cancellationToken)
        {
            return await _context.Reviews.ToListAsync();
        }
    }
}
