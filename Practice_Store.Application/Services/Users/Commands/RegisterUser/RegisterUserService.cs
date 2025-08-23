using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUser
    {

        private readonly UserManager<IdtUser> _userManager;
        private readonly RoleManager<IdtRole> _roleManager;
        public RegisterUserService(UserManager<IdtUser> userManager, RoleManager<IdtRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ResultRegisterUserDto ValidateUser(RequestRegisterUserDto Request)
        {
            try
            {
                var ValidationResult = RegisterUserValidation.CheckValidation(Request);
                if (!ValidationResult.IsSuccess)
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = ValidationResult.Message,
                        StatusCode = ValidationResult.StatusCode
                    };

                var GetEmail = _userManager.Users.IgnoreQueryFilters().Where(u => u.Email.ToLower() == Request.Email.ToLower()).FirstOrDefault();
                if (GetEmail != null)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "این پست الکترونیک قبلا استفاده شده است",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                foreach (var role in Request.Roles)
                {
                    var CheckRole = _roleManager.FindByNameAsync(role).Result;
                    if (CheckRole == null)
                    {
                        return new ResultRegisterUserDto
                        {
                            IsSuccess = false,
                            Message = "نقش یافت نشد",
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                }

                IdtUser User = new IdtUser
                {
                    Name = Request.Name,
                    LastName = Request.LastName,
                    Email = Request.Email,
                    Address = Request.Address,
                    PostCode = Request.PostCode,
                    EmailConfirmed = false,
                    InsertTime = DateTime.UtcNow,
                    UserName = Request.Email
                };
                var Result = _userManager.CreateAsync(User, Request.Password).Result;

                if (!Result.Succeeded)
                {
                    string ErrorMessage = "";
                    foreach (var Error in Result.Errors.ToList())
                        ErrorMessage += Error.Description + Environment.NewLine;

                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = ErrorMessage,
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };

                var Acivate = _userManager.SetLockoutEnabledAsync(User, false).Result;

                var AddRole = _userManager.AddToRolesAsync(User, Request.Roles).Result;
                if (!AddRole.Succeeded)
                {
                    var _user = _userManager.FindByIdAsync(User.Id).Result;
                    if (_user != null)
                    {
                        var Delete = _userManager.DeleteAsync(_user).Result;
                    }
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "ثبت نام ناموفق !!!",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var Token = _userManager.GenerateEmailConfirmationTokenAsync(User).Result;
                return new ResultRegisterUserDto
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ثبت شد",
                    StatusCode = StatusCodes.Status202Accepted,
                    UserId = User.Id,
                    UserEmail = User.Email,
                    EmailValidationToken = Token
                };
            }

            catch (Exception)
            {
                var _user = _userManager.FindByEmailAsync(Request.Email).Result;
                if (_user != null)
                {
                    var Delete = _userManager.DeleteAsync(_user).Result;
                }
                return new ResultRegisterUserDto
                {
                    IsSuccess = false,
                    Message = "ثبت نام ناموفق !!!",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }

        public ResultRegisterUserDto ValidateEmail(string UserId, string EmailValidationToken)
        {
            try
            {
                var User = _userManager.FindByIdAsync(UserId).Result;
                if (User == null)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                var ConfEmail = _userManager.ConfirmEmailAsync(User, EmailValidationToken).Result;
                if (!ConfEmail.Succeeded)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "کاربر تایید نشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }

                return new ResultRegisterUserDto
                {
                    IsSuccess = true,
                    Message = "ایمیل شما با موفقیت تایید شد",
                    StatusCode = StatusCodes.Status200OK,
                    UserId = User.Id,
                };
            }
            catch (Exception)
            {
                return new ResultRegisterUserDto
                {
                    IsSuccess = false,
                    Message = "ثبت نام ناموفق !!!",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
