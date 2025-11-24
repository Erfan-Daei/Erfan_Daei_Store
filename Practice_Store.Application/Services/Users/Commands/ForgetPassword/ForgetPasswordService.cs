using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public partial class ForgetPasswordService : IForgetPassword
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IForgetPasswordRepo _forgetPasswordRepo;
        public ForgetPasswordService(IUserRepoFinder userRepoFinder,
            IForgetPasswordRepo forgetPasswordRepo)
        {
            _userRepoFinder = userRepoFinder;
            _forgetPasswordRepo = forgetPasswordRepo;
        }
        public ResultForgetPasswordDto CheckPassword(string UserId, string NewPassword)
        {
            string PasswordPattern = @"^(?=.*\b\w+\b){8,}(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*(),.?"":{}|<>]).+$";
            var MatchPassword = Regex.Match(NewPassword, PasswordPattern);
            if (!MatchPassword.Success)
            {
                return new ResultForgetPasswordDto
                {
                    IsSuccess = false,
                    Message = "رمز عبور باید حداقل شامل 8 حرف، یک حرف بزرگ، یک حرف کوچک، یک عدد و یک حرف خاص باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var _User = _userRepoFinder.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultForgetPasswordDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            bool ConfirmedEmail = _User.EmailConfirmed;
            if (!ConfirmedEmail)
            {
                return new ResultForgetPasswordDto
                {
                    IsSuccess = false,
                    Message = "لطفا ابتدا ایمیل خودرا تایید کنید",
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            var Token = _forgetPasswordRepo.GeneratePasswordResetToken(_User);

            return new ResultForgetPasswordDto
            {
                IsSuccess = true,
                Message = "تاییدیه ایمیل برای شم فرستاده شد",
                StatusCode = StatusCodes.Status202Accepted,
                Token = Token,
                Email = _User.Email,
            };
        }

        public ResultDto UpdatePassword(string UserId, string Token, string NewPassword)
        {

            string PasswordPattern = @"^(?=.*\b\w+\b){8,}(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*(),.?"":{}|<>]).+$";
            var MatchPassword = Regex.Match(NewPassword, PasswordPattern);
            if (!MatchPassword.Success)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "رمز عبور باید حداقل شامل 8 حرف، یک حرف بزرگ، یک حرف کوچک، یک عدد و یک حرف خاص باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var _User = _userRepoFinder.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultForgetPasswordDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var CheckToken = _forgetPasswordRepo.ResetPassword(_User, Token, NewPassword);
            if (!CheckToken.Succeeded)
            {
                return new ResultForgetPasswordDto
                {
                    IsSuccess = false,
                    Message = "عملیات ناموفق",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            return new ResultForgetPasswordDto
            {
                IsSuccess = true,
                Message = "رمز عبور بروز شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }

}
