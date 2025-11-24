using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.LogOut
{
    public class LogOutService : ILogOut
    {
        private readonly ILogOutRepo _logOutRepo;
        public LogOutService(ILogOutRepo logOutRepo)
        {
            _logOutRepo = logOutRepo;
        }

        public ResultDto Execute(string UserId)
        {
            var DeleteToken = _logOutRepo.RemoveUserToken(UserId);
            if (!DeleteToken)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "اروری رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت خارج شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
