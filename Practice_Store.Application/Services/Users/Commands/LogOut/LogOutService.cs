using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.LogOut
{
    public class LogOutService : ILogOut
    {
        private readonly IDatabaseContext _databaseContext;

        public LogOutService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(string UserId)
        {
            var Token = _databaseContext.UserTokens.Where(t => t.UserId == UserId && t.Name == nameof(TokenType.AccessToken)).ToList();
            _databaseContext.UserTokens.RemoveRange(Token);
            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت خارج شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
