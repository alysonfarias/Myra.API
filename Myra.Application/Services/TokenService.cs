using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Myra.Application.Extensions;
using Myra.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Myra.Application.Services
{
    public class TokenService : ITokenService
    {

        private readonly AppSettings _settings;

        public TokenService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _settings.Emissary,
                Audience = _settings.ValidOn,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

