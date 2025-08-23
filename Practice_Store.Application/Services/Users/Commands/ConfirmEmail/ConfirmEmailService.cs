using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailService : IConfirmEmail
    {
        private readonly UserManager<IdtUser> _userManager;
        public ConfirmEmailService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }
        public ResultDto ConfirmEmail(string UserId, string Token)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;

            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                };
            }

            var Confirm = _userManager.ConfirmEmailAsync(_User, Token).Result;

            if (!Confirm.Succeeded)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "ایمیل کاربر تایید شد",
                StatusCode = StatusCodes.Status200OK,
            };

        }

        public ResultDto<string> GenerateToken(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;

            if (_User == null)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var Token = _userManager.GenerateEmailConfirmationTokenAsync(_User).Result;

            return new ResultDto<string>()
            {
                Data = Token,
                IsSuccess = true,
                Message = $"تاییدیه ایمیل به حساب {_User.Email} ارسال شد",
                StatusCode = StatusCodes.Status202Accepted,
            };

        }
    }
}
