using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Interfaces;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Application.Auth;

namespace Sa3aBeSa3a.Application.Services
{
    public class TokenServices(IOptions<JwtOptions> options) : ITokenServices
    {
        private static readonly JsonWebTokenHandler Handler = new();
        public TokenResult Create(Members member)
        {
            var o = options.Value;
            var expires = DateTime.UtcNow.AddMinutes(o.ExpiryMinutes);

            var claims = new Dictionary<string, object>
            {
                [ClaimNames.MemberId] = member.Id.ToString(),
                [ClaimNames.Name] = member.Name,
                [ClaimNames.Role] = member.Role.ToString()
            };


            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = o.Issuer,
                Audience = o.Audience,
                Expires = expires,
                Claims = claims,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(o.Key)), SecurityAlgorithms.HmacSha256)
            };

            return new TokenResult(Handler.CreateToken(descriptor), expires);
        }
    }
}
