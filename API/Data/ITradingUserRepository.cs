using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.TradingUserDtos;
using API.Entities;

namespace API.Data
{
    public interface ITradingUserRepository: IGenericRepository<TradingUser>
    {
        Task<bool> UserExists(string userName);
        Task<IEnumerable<TradingUser>> GetTradingUsersAsync();
        Task<TradingUser?> GetTradingUserByIdAsync(int id);
        
    }
}