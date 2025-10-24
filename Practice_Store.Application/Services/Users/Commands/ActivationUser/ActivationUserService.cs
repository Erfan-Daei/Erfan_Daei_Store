using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ActivationUser
{
    public class ActivationUserService : IActivationUser
    {
        private readonly IManageUserRepository _manageUserRepository;
        public ActivationUserService(IManageUserRepository manageUserRepository)
        {
            _manageUserRepository = manageUserRepository;
        }
        public ResultDto<ResultActivationUserDto> ChangeActivationState(string UserId)
        {
            var _User = _manageUserRepository.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultDto<ResultActivationUserDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var Change = _manageUserRepository.ChangeUserActivation(_User);
            if (!Change)
            {
                return new ResultDto<ResultActivationUserDto>()
                {
                    IsSuccess = false,
                    Message = "عملیات ناموفق",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
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
