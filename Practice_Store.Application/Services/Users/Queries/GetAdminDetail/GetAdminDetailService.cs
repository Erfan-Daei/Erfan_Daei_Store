using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetAdminDetail
{
    public class GetAdminDetailService : IGetAdminDetail
    {
        private readonly IUserRepoFinder _userRepoFinder;
        public GetAdminDetailService(IUserRepoFinder userRepoFinder)
        {
            _userRepoFinder = userRepoFinder;
        }

        public ResultDto<GetAdminDetailDto> GetDetail(string UserId)
        {
            var _User = _userRepoFinder.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultDto<GetAdminDetailDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _userRepoFinder.GetRoles(_User);

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
