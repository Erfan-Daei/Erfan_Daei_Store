using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetUserRoles
{
    public interface IGetUserRoles
    {
        ResultDto<List<string>> GetUserRoles(string UserId);
    }
}
