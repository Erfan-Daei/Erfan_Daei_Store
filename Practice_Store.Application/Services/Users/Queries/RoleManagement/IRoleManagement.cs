using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public interface IRoleManagement
    {
        ResultDto<ResultRoleManagement_GetRolesDto> GetRoles(RequestRoleManagement_GetRolesDto Request);
        ResultDto<RoleManagement_RoleDto> GetRoleDetail(string RoleName);
        ResultDto AddRole(string RoleName);
        ResultDto EditRole(string RoleName, string NewRoleName);
        ResultDto DeleteRole(string RoleName);
    }
}
