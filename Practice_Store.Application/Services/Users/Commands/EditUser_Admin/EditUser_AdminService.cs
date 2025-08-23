using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.EditUser_Admin
{
    public class EditUser_AdminService : IEditUser_Admin
    {
        private readonly UserManager<IdtUser> _userManager;
        public EditUser_AdminService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }
        public ResultDto EditUser(RequestEditUser_AdminDto Request)
        {
            var _User = _userManager.FindByIdAsync(Request.UserId).Result;

            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var Validate = EditUser_AdminValidation.Validate(Request);
            if (!Validate.IsSuccess)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = Validate.Message,
                    StatusCode = Validate.StatusCode,
                };
            }

            if (!string.IsNullOrEmpty(Request.Email))
            {
                var CheckEmailExist = _userManager.Users.IgnoreQueryFilters().Where(u => u.Email.ToLower() == Request.Email.ToLower()).FirstOrDefault();
                if (CheckEmailExist != null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "آین ایمیل قبلا استفاده شده",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                _User.Email = Request.Email;
            }

            if (!string.IsNullOrEmpty(Request.Password))
            {
                var ResetToken = _userManager.GeneratePasswordResetTokenAsync(_User).Result;
                _userManager.ResetPasswordAsync(_User, ResetToken, Request.Password);
            }

            if (!string.IsNullOrEmpty(Request.Name))
            {
                _User.Name = Request.Name;
            }

            if (!string.IsNullOrEmpty(Request.LastName))
            {
                _User.LastName = Request.LastName;
            }

            if (!string.IsNullOrEmpty(Request.Address))
            {
                _User.Address = Request.Address;
            }

            if (!string.IsNullOrEmpty(Request.PhoneNumber))
            {
                _User.PhoneNumber = Request.PhoneNumber;
                _User.PhoneNumberConfirmed = false;
            }

            if (!string.IsNullOrEmpty(Request.PostCode))
            {
                _User.PostCode = Convert.ToInt64(Request.PostCode);
            }

            _User.UpdateTime = DateTime.UtcNow;

            var Update = _userManager.UpdateAsync(_User).Result;
            if (!Update.Succeeded)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات بروز شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
