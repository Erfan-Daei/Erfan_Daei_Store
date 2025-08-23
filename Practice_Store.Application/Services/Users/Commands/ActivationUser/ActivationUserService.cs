using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.ActivationUser
{
    public class ActivationUserService : IActivationUser
    {
        private readonly UserManager<IdtUser> _userManager;
        public ActivationUserService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }
        public ResultDto<ResultActivationUserDto> ChangeActivationState(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultDto<ResultActivationUserDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var _Change = _userManager.SetLockoutEnabledAsync(_User, !_User.LockoutEnabled);
            string UserState = _User.LockoutEnabled == true ? "غیر فعال" : "فعال";

            return new ResultDto<ResultActivationUserDto>()
            {
                Data = new ResultActivationUserDto
                {
                    UserEmail = _User.Email,
                    UserState = UserState,
                },
                IsSuccess = true,
                Message = $"کاربر {UserState} شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
