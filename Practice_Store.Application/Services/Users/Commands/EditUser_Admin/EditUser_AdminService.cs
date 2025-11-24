using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUser_Admin
{
    public class EditUser_AdminService : IEditUser_Admin
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IEditUser_AdminRepo _editUser_AdminRepo;
        public EditUser_AdminService(IUserRepoFinder userRepoFinder,
            IEditUser_AdminRepo editUser_AdminRepo)
        {
            _userRepoFinder = userRepoFinder;
            _editUser_AdminRepo = editUser_AdminRepo;
        }
        public ResultDto EditUser(RequestEditUser_AdminDto Request)
        {
            var _User = _userRepoFinder.FindUserById(Request.UserId);

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
                var CheckEmailExist = _editUser_AdminRepo.EmailExist(Request.Email);
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
                var ResetPassword = _editUser_AdminRepo.ResetPassword(_User, Request.Password);
                if (!ResetPassword.Succeeded)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "خطایی رخ داد",
                        StatusCode = StatusCodes.Status500InternalServerError,
                    };
                }
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

            var Update = _userRepoFinder.UpdateUser(_User);

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
