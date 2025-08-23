using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.DeleteUser
{
    public class DeleteUserService : IDeleteUser
    {
        private readonly UserManager<IdtUser> _userManager;
        public DeleteUserService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }
        public ResultDto DeleteUser(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _userManager.GetRolesAsync(_User).Result;
            var _DeleteRoles = _userManager.RemoveFromRolesAsync(_User, _UserRoles).Result;
            _User.DeletedTime = DateTime.UtcNow;
            _User.IsDeleted = true ;
            _User.LockoutEnabled = true ;
            var Delete = _userManager.UpdateAsync(_User).Result;
            if (!Delete.Succeeded)
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
                Message = "کاربر با موفقیت حذف شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
