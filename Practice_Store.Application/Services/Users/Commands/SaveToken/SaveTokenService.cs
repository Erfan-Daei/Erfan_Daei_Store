using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.SaveToken
{
    public class SaveTokenService : ISaveToken
    {
        private readonly ISaveTokenRepo _saveTokenRepo;
        private readonly IGenerateToken _generateToken;
        public SaveTokenService(ISaveTokenRepo saveTokenRepo,
            IGenerateToken generateToken)
        {
            _saveTokenRepo = saveTokenRepo;
            _generateToken = generateToken;
        }

        public ResultDto<(string, string)> SaveToken(string UserId, string Email, List<string> Roles)
        {
            try
            {
                (string, string) Tokens = _generateToken.GenerateToken(UserId, Email, Roles);

                var DatabaseJwtToken = _generateToken.GenerateIdtUserToken(UserId, Tokens.Item1, Tokens.Item2);

                var AddToken = _saveTokenRepo.AddToken(DatabaseJwtToken);

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
