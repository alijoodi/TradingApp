using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
         string CreateToken(TradingUser user);
    }
}