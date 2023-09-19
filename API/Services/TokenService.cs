using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            var tokenKey = configuration["TokenKey"];

            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentException("TokenKey is null or empty");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
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