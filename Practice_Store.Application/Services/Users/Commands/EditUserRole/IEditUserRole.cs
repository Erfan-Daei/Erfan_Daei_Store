using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUserRole
{
    public interface IEditUserRole
    {
        ResultDto AddRoles(RequestEditUserRole Request);
        ResultDto DeleteRoles(RequestEditUserRole Request);
    }
}
