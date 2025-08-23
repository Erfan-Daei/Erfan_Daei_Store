using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRoles
    {
        ResultDto<List<RolesDto>> Execute();
    }
}
