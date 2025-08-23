using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Domain.Entities.Users;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public class ChangeUserEmail_SiteService : IChangeUserEmail_Site
    {
        private readonly UserManager<IdtUser> _userManager;
        public ChangeUserEmail_SiteService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public ResultChangeUserEmail_SiteDto CheckEmailValidation(string UserId, string LastEmail, string NewEmail)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            if (LastEmail.ToLower() == NewEmail.ToLower())
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "لطفا ایمیل جدید را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            string EmailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            var MatchEmail = Regex.Match(NewEmail, EmailPattern);
            if (!MatchEmail.Success)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "لطفا پست الکترونیک را به درستی وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var _GetEmail = _userManager.Users.IgnoreQueryFilters().FirstOrDefault(p => p.Email.ToLower() == NewEmail.ToLower());
            if (_GetEmail != null)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "این ایمیل از قبل استفاده شده است",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            _User.Email = NewEmail;
            _User.UserName = NewEmail;
            _User.EmailConfirmed = false;
            var Update = _userManager.UpdateAsync(_User).Result;
            if (!Update.Succeeded)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var Token = _userManager.GenerateEmailConfirmationTokenAsync(_User).Result;

            return new ResultChangeUserEmail_SiteDto
            {
                IsSuccess = true,
                Message = "تاییدیه به پست الکترونیک جدید شما ارسال گردید",
                StatusCode = StatusCodes.Status202Accepted,
                EmailValidationToken = Token,
            };
        }
    }
}
