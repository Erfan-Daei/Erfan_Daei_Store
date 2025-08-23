using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.LogInUsers
{
    public class LogInUserService : ILogInUser
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly IDatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public LogInUserService(UserManager<IdtUser> userManager,
            IGenerateToken generateToken,
            IDatabaseContext databaseContext,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _configuration = configuration;
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

            var _User = _userManager.FindByEmailAsync(Email).Result;

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
            var VerifiedPassword = _userManager.CheckPasswordAsync(_User, Password).Result;

            if (!VerifiedPassword)
            {
                return new ResultDto<ResultLogInUserDto>()
                {
                    IsSuccess = false,
                    Message = "رمزعبور اشتباه وارد شد!",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var PreviousToken = _databaseContext.UserTokens.Where(t => t.Name == nameof(TokenType.AccessToken) && t.UserId == _User.Id).ToList();
            _databaseContext.UserTokens.RemoveRange(PreviousToken);

            List<string> Roles = _userManager.GetRolesAsync(_User).Result.ToList();

            _databaseContext.SaveChanges();

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
