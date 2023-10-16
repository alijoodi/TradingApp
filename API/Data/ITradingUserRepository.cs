using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;
using API.Helpers;

namespace API.Data
{
    public interface ITradingUserRepository: IGenericRepository<TradingUser>
    {
        Task<bool> UserExists(string userName);
        Task<PagedList<TradingUserDto>> GetTradingUsersAsync(UserParams userParams);
        Task<TradingUserDto?> GetTradingUserByIdAsync(int id);
        Task<TradingUserDto?> GetTradingUserByUsernameAsync(string username);
        Task<TradingUserDto?> DeactiveTradingUserByUsernameAsync(string username);
        
    }
}