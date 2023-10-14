using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            var tokenKey = configuration["TokenKey"];
            this._userManager = userManager;

            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentException("TokenKey is null or empty");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var expirationDate = DateTime.UtcNow.AddDays(7); // Set the expiration to `DateTime.UtcNow.AddDays(expiresInDays)`

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenOptions);
        }

        public string CreateToken(TradingUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var expirationDate = DateTime.UtcNow.AddDays(7); // Set the expiration to `DateTime.UtcNow.AddDays(expiresInDays)`

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenOptions);
        }
    }
}