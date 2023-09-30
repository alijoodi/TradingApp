using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TradingUserRepository : GenericRepository<TradingUser>, ITradingUserRepository
    {
        public TradingUserRepository(DataContext context) : base(context)
        {
        }

        public async Task<TradingUser?> GetTradingUserByIdAsync(int id)
        {
            return await _context.TradingUsers.Where(x => id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TradingUser>> GetTradingUsersAsync()
        {
            return await _context.TradingUsers.Where(x => true).ToListAsync();
        }

        public async Task<bool> UserExists(string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);

            return await _context.TradingUsers.AnyAsync(user => user.Username == userName.ToLower());
        }

    }
}