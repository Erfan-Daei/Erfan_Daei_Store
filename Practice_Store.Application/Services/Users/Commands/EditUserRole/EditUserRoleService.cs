using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using System.Data;

namespace Practice_Store.Application.Services.Users.Commands.EditUserRole
{
    public class EditUserRoleService : IEditUserRole
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly RoleManager<IdtRole> _roleManager;
        public EditUserRoleService(UserManager<IdtUser> userManager, RoleManager<IdtRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ResultDto AddRoles(RequestEditUserRole Request)
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

            var _UserRoles = _userManager.GetRolesAsync(_User).Result.ToList();
            Request.Roles = Request.Roles.Except(_UserRoles).ToList();

            if (Request.Roles.Count == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا نقش جدیدی انتخاب کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            foreach (var role in Request.Roles)
            {
                var CheckRole = _roleManager.RoleExistsAsync(role).Result;
                if (!CheckRole)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = $"نقش {role} یافت نشد",
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                }
            }

            var AddRoles = _userManager.AddToRolesAsync(_User, Request.Roles).Result;
            if (!AddRoles.Succeeded)
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
                Message = "نقش های جدید با موفقیت برای کاربر ثبت شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public ResultDto DeleteRoles(RequestEditUserRole Request)
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

            foreach (var role in Request.Roles)
            {
                var CheckRole = _roleManager.RoleExistsAsync(role).Result;
                if (!CheckRole)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = $"نقش {role} یافت نشد",
                        StatusCode = StatusCodes.Status404NotFound,
                    };
                }
            }

            var DeleteRole = _userManager.RemoveFromRolesAsync(_User, Request.Roles).Result;
            if (!DeleteRole.Succeeded)
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
                Message = "نقش های کاربر با موفقیت حذف شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
