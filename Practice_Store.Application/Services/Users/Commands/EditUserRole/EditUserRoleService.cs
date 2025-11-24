using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUserRole
{
    public class EditUserRoleService : IEditUserRole
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IEditUserRoleRepo _editUserRoleRepo;
        public EditUserRoleService(IUserRepoFinder userRepoFinder,
            IEditUserRoleRepo editUserRoleRepo)
        {
            _userRepoFinder = userRepoFinder;
            _editUserRoleRepo = editUserRoleRepo;
        }

        public ResultDto AddRoles(RequestEditUserRole Request)
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

            var _UserRoles = _userRepoFinder.GetRoles(_User);
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
                var CheckRole = _editUserRoleRepo.RoleExist(role);
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

            var AddRoles = _editUserRoleRepo.AddToRoles(_User, Request.Roles);
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

            foreach (var role in Request.Roles)
            {
                var CheckRole = _editUserRoleRepo.RoleExist(role);
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

            var DeleteRole = _editUserRoleRepo.RemoveFromRoles(_User, Request.Roles);
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
