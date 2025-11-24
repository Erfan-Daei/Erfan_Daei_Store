using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailService : IConfirmEmail
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IConfirmEmailRepo _confirmEmailRepo;
        public ConfirmEmailService(IUserRepoFinder userRepoFinder,
            IConfirmEmailRepo confirmEmailRepo)
        {
            _userRepoFinder = userRepoFinder;
            _confirmEmailRepo = confirmEmailRepo;
        }
        public ResultDto ConfirmEmail(string UserId, string Token)
        {
            var _User = _userRepoFinder.FindUserById(UserId);

            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                };
            }

            var Confirm = _confirmEmailRepo.ConfirmEmail(_User, Token);

            if (!Confirm)
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
            var _User = _userRepoFinder.FindUserById(UserId);

            if (_User == null)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var Token = _confirmEmailRepo.GenerateToken(_User);

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
