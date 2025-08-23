using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetAdminDetail
{
    public interface IGetAdminDetail
    {
        ResultDto<GetAdminDetailDto> GetDetail(string UserId);
    }
}
