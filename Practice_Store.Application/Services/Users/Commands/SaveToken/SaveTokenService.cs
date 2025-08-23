using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.SaveToken
{
    public class SaveTokenService : ISaveToken
    {
        private readonly IGenerateToken _generateToken;
        private readonly IDatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public SaveTokenService(IGenerateToken generateToken, IDatabaseContext databaseContext, IConfiguration configuration)
        {
            _generateToken = generateToken;
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public ResultDto<(string, string)> SaveToken(string UserId, string Email, List<string> Roles)
        {
            try
            {
                (string, string) Tokens = _generateToken.GenerateToken(UserId, Email, Roles);

                var Token = new IdtUsertokens
                {
                    TokenId = Guid.NewGuid(),
                    LoginProvider = "Internal",
                    Name = nameof(TokenType.AccessToken),
                    TokenExpireDate = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:expire"])),
                    UserId = UserId,
                    Value = HashHelper.Hash(Tokens.Item1),
                    RefreshToken = HashHelper.Hash(Tokens.Item2),
                    RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:refreshExpire"])),
                };

                _databaseContext.UserTokens.Add(Token);
                _databaseContext.SaveChanges();

                return new ResultDto<(string, string)>()
                {
                    Data = Tokens,
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                };
            }
            catch
            {
                return new ResultDto<(string, string)>()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
