using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Services.Users.Queries.RoleManagement;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries
{
    public interface IRoleManagementRepo
    {
        IdentityResult CreateRole(IdtRole role);
        IdtRole? FindByName(string name);
        IdentityResult DeleteRole(IdtRole role);
        IdentityResult UpdateRole(IdtRole role);
        List<IdtRole> SearchRoles(RequestRoleManagement_GetRolesDto Request);
    }
}
