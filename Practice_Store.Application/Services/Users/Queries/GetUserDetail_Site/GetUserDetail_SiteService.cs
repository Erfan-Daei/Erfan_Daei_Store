using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetUserDetail_Site
{
    public class GetUserDetail_SiteService : IGetUserDetail_Site
    {
        private readonly IUserRepoFinder _userRepoFinder;
        public GetUserDetail_SiteService(IUserRepoFinder userRepoFinder)
        {
            _userRepoFinder = userRepoFinder;
        }

        public ResultDto<GetUserDetail_SiteDto> GetUser(string UserId)
        {
            var _User = _userRepoFinder.FindUserById(UserId);
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
