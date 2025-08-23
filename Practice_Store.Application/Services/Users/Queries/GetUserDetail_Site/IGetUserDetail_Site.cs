using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetUserDetail_Site
{
    public interface IGetUserDetail_Site
    {
        ResultDto<GetUserDetail_SiteDto> GetUser(string UserId);
    }
}
