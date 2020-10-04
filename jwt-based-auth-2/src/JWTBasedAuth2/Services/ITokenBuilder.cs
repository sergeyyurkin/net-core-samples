using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTBasedAuth2.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(string username);
    }

    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: credentials);

            var jwtEncoded = new JwtSecurityTokenHandler().WriteToken(jwt);

            return jwtEncoded;
        }
    }
}
