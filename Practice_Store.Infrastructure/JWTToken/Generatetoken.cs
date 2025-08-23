using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Practice_Store.Application.JWTToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice_Store.Infrastructure.JWTToken
{
    public class Generatetoken : IGenerateToken
    {
        private readonly IConfiguration _configuration;

        public Generatetoken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string,string) GenerateToken(string UserId, string UserEmail, List<string> UserRoles)
        {
            var _Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, UserId),
                new Claim(ClaimTypes.Email, UserEmail),
            };

            foreach (var role in UserRoles)
            {
                _Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = _configuration["JWTConfig:key"];
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var Credential = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTConfig:issuer"],
                audience: _configuration["JWTConfig:audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:expire"])),
                notBefore: DateTime.UtcNow,
                claims: _Claims,
                signingCredentials: Credential
                );

            var _JWTToken = new JwtSecurityTokenHandler().WriteToken(Token);
            var _RefreshToken = Guid.NewGuid().ToString();

            return (_JWTToken,_RefreshToken);
        }
    }
}
