using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Queries.GetUserRoles
{
    public class GetUserRolesService : IGetUserRoles
    {
        private readonly UserManager<IdtUser> _userManager;
        public GetUserRolesService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto<List<string>> GetUserRoles(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultDto<List<string>>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _userManager.GetRolesAsync(_User).Result.ToList();

            return new ResultDto<List<string>>()
            {
                Data = _UserRoles,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
