using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.RefreshToken
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdtUser> _userManager;
        private readonly IGenerateToken _generateToken;

        public RefreshTokenService(IDatabaseContext databaseContext,
            IConfiguration configuration,
            UserManager<IdtUser> userManager,
            IGenerateToken generateToken)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
            _userManager = userManager;
            _generateToken = generateToken; 
        }

        public ResultDto<(string, string)> Execute(string RefreshToken)
        {
            var Token = _databaseContext.UserTokens.FirstOrDefault(x => x.RefreshToken == HashHelper.Hash(RefreshToken));
            if (Token == null)
            {
                return new ResultDto<(string, string)>()
                {
                    IsSuccess = false,
                    Message = "توکن کاربر وجود ندارد",
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }
            
            if (Token.RefreshTokenExpireDate < DateTime.UtcNow)
            {
                return new ResultDto<(string, string)>()
                {
                    IsSuccess = false,
                    Message = "توکن کاربر منقضی شده است",
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            var PreviousToken = _databaseContext.UserTokens.Where(t => t.Name == nameof(TokenType.AccessToken) && t.UserId == Token.UserId).ToList();
            _databaseContext.UserTokens.RemoveRange(PreviousToken);
            _databaseContext.SaveChanges();

            var _User = _userManager.FindByIdAsync(Token.UserId).Result;
            List<string> Roles = _userManager.GetRolesAsync(_User).Result.ToList();

            (string, string) Tokens = _generateToken.GenerateToken(_User.Id, _User.Email, Roles);


            var JwtToken = new IdtUsertokens
            {
                TokenId = Guid.NewGuid(),
                LoginProvider = "Internal",
                Name = nameof(TokenType.AccessToken),
                TokenExpireDate = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:expire"])),
                UserId = _User.Id,
                Value = HashHelper.Hash(Tokens.Item1),
                RefreshToken = HashHelper.Hash(Tokens.Item2),
                RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:refreshExpire"])),
            };

            _databaseContext.UserTokens.Add(JwtToken);
            _databaseContext.SaveChanges();

            return new ResultDto<(string, string)>()
            {
                Data = Tokens,
                IsSuccess = true,
                Message = "توکن جدید صادر شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
