using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public interface IRoleManagement
    {
        ResultDto<ResultRoleManagement_GetRolesDto> GetRoles(RequestRoleManagement_GetRolesDto Request);
        ResultDto<RoleManagement_RoleDto> GetRoleDetail(RequestRoleManagementDto Request);
        ResultDto AddRole(RequestRoleManagementDto Request);
        ResultDto EditRole(RequestRoleManagementDto Request);
        ResultDto DeleteRole(RequestRoleManagementDto Request);
    }
}
