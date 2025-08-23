using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Queries.GetAdminDetail
{
    public class GetAdminDetailService : IGetAdminDetail
    {
        private readonly UserManager<IdtUser> _userManager;
        public GetAdminDetailService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto<GetAdminDetailDto> GetDetail(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultDto<GetAdminDetailDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _userManager.GetRolesAsync(_User).Result.ToList();

            return new ResultDto<GetAdminDetailDto>()
            {
                Data = new GetAdminDetailDto()
                {
                    Id = UserId,
                    Email = _User.Email,
                    EmailConfirmed = _User.PhoneNumberConfirmed,
                    Name = _User.Name,
                    LastName = _User.LastName,
                    Address = _User.Address,
                    PostCode = _User.PostCode,
                    Mobile = _User.PhoneNumber,
                    MobileConfirmed = _User.PhoneNumberConfirmed,
                    IsActive = _User.LockoutEnabled == true ? "غیر فعال" : "فعال",
                    Roles = _UserRoles
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
