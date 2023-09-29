using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TradingUserRepository : GenericRepository<TradingUser>, ITradingUserRepository
    {
        private readonly IMapper _mapper;
        public TradingUserRepository(DataContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public async Task<TradingUserDto?> GetTradingUserByIdAsync(int id)
        {
            return await _context.TradingUsers.Where(x => id == id).ProjectTo<TradingUserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TradingUserDto>> GetTradingUsersAsync()
        {
            return await _context.TradingUsers.Where(x => true).ProjectTo<TradingUserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> UserExists(string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);

            return await _context.TradingUsers.AnyAsync(user => user.Username == userName.ToLower());
        }

    }
}