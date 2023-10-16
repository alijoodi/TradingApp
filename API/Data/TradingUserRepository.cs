using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TradingUserRepository : GenericRepository<TradingUser>, ITradingUserRepository
    {
        public TradingUserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<TradingUserDto?> DeactiveTradingUserByUsernameAsync(string username)
        {
            var tradingUser = await _context.TradingUsers.Where(x => x.Username == username).FirstOrDefaultAsync();
            if (tradingUser != null)
            {
                tradingUser.IsActive = !tradingUser.IsActive;
                return _mapper.Map<TradingUserDto>(tradingUser);
            }
            else
            {
                throw new Exception("User not exist");
            }
        }

        public async Task<TradingUserDto?> GetTradingUserByIdAsync(int id)
        {
            return await _context.TradingUsers.Where(x => x.Id == id).ProjectTo<TradingUserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<TradingUserDto?> GetTradingUserByUsernameAsync(string username)
        {
            return await _context.TradingUsers.Where(x => x.Username == username).ProjectTo<TradingUserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<PagedList<TradingUserDto>> GetTradingUsersAsync(UserParams userParams)
        {
            var query = _context.TradingUsers
            .ProjectTo<TradingUserDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();

            return await PagedList<TradingUserDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> UserExists(string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);

            return await _context.TradingUsers.AnyAsync(user => user.Username == userName.ToLower());
        }

    }
}