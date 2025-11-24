using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.RefreshToken
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IGenerateToken _generateToken;

        public RefreshTokenService(IUserRepoFinder userRepoFinder,
            IRefreshTokenRepo refreshTokenRepo,
            IGenerateToken generateToken)
        {
            _userRepoFinder = userRepoFinder;
            _refreshTokenRepo = refreshTokenRepo;
            _generateToken = generateToken;
        }

        public ResultDto<(string, string)> Execute(string RefreshToken)
        {
            var Token = _refreshTokenRepo.ChechRefreshToken(RefreshToken);
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

            var RemovePreviousToken = _refreshTokenRepo.RemovePreviousToken(Token.UserId);

            var _User = _userRepoFinder.FindUserById(Token.UserId);
            List<string> Roles = _userRepoFinder.GetRoles(_User);

            (string, string) Tokens = _generateToken.GenerateToken(_User.Id, _User.Email, Roles);


            IdtUsertokens DatabaseJwtToken = _generateToken.GenerateIdtUserToken(_User.Id, Tokens.Item1, Tokens.Item2);

            var AddToken = _refreshTokenRepo.AddToken(DatabaseJwtToken);

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
