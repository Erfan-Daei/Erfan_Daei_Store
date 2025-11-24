using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetUserRoles
{
    public class GetUserRolesService : IGetUserRoles
    {
        private readonly IUserRepoFinder _userRepoFinder;
        public GetUserRolesService(IUserRepoFinder userRepoFinder)
        {
            _userRepoFinder = userRepoFinder;
        }

        public ResultDto<List<string>> GetUserRoles(string UserId)
        {
            var _User = _userRepoFinder.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultDto<List<string>>()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _userRepoFinder.GetRoles(_User);

            return new ResultDto<List<string>>()
            {
                Data = _UserRoles,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
