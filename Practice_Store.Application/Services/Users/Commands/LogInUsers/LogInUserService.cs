using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.LogInUsers
{
    public class LogInUserService : ILogInUser
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly ILogInUserRepo _logInUserRepo;
        public LogInUserService(IUserRepoFinder userRepoFinder,
            ILogInUserRepo logInUserRepo)
        {
            _userRepoFinder = userRepoFinder;
            _logInUserRepo = logInUserRepo;
        }

        public ResultDto<ResultLogInUserDto> Execute(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "لطفا پست الکترونیک و رمز عبور را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var _User = _userRepoFinder.FindUserByEmail(Email);

            if (_User == null)
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (_User.LockoutEnabled == true)
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "دسترسی شما توسط ادمین بسته شده است",
                    StatusCode = StatusCodes.Status403Forbidden,
                };
            }
            var VerifiedPassword = _logInUserRepo.CheckPassword(_User, Password);

            if (!VerifiedPassword)
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "رمزعبور اشتباه وارد شد!",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var DeletePreviousTokens = _logInUserRepo.DeletePreviousTokens(_User.Id);
            if (!DeletePreviousTokens)
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            List<string> Roles = _userRepoFinder.GetRoles(_User);
            _logInUserRepo.Save();

            return new ResultDto<ResultLogInUserDto>()
            {
                Data = new ResultLogInUserDto
                {
                    UserId = _User.Id,
                    Email = _User.Email,
                    Roles = Roles,
                    FullName = _User.Name + " " + _User.LastName,
                },
                IsSuccess = true,
                Message = "ورود با موفقیت انجام شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
