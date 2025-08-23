using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Queries.GetUserDetail_Site
{
    public class GetUserDetail_SiteService : IGetUserDetail_Site
    {
        private readonly UserManager<IdtUser> _userManager;
        public GetUserDetail_SiteService(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto<GetUserDetail_SiteDto> GetUser(string UserId)
        {
            var _User = _userManager.FindByIdAsync(UserId).Result;
            if (_User == null)
            {
                return new ResultDto<GetUserDetail_SiteDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            return new ResultDto<GetUserDetail_SiteDto>()
            {
                Data = new GetUserDetail_SiteDto()
                {
                    Id = UserId,
                    Name = _User.Name,
                    LastName = _User.LastName,
                    Address = _User.Address,
                    PostCode = _User.PostCode,
                    Mobile = _User.PhoneNumber,
                    Email = _User.Email,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
