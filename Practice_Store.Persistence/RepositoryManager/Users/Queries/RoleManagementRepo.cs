using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Application.Services.Users.Queries.RoleManagement;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using System.Data;

namespace Practice_Store.Persistence.RepositoryManager.Users.Queries
{
    public class RoleManagementRepo : IRoleManagementRepo
    {
        private readonly RoleManager<IdtRole> _roleManager;
        public RoleManagementRepo(RoleManager<IdtRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IdentityResult CreateRole(IdtRole role)
        {
            return _roleManager.CreateAsync(role).Result;
        }

        public IdtRole? FindByName(string name)
        {
            return _roleManager.FindByNameAsync(name).Result;
        }

        public IdentityResult DeleteRole(IdtRole role)
        {
            return _roleManager.DeleteAsync(role).Result;
        }

        public IdentityResult UpdateRole(IdtRole role)
        {
            return _roleManager.UpdateAsync(role).Result;
        }
        public List<IdtRole> SearchRoles(RequestRoleManagement_GetRolesDto Request)
        {
            return _roleManager.Roles
                .Where(r => string.IsNullOrEmpty(Request.SearchKey) ||
                r.Name.Contains(Request.SearchKey) ||
                r.NormalizedName.Contains(Request.SearchKey))
                .ToPaged(Request.Page ?? 1, Request.PageSize ?? 20)
                .ToList();
        }
    }
}
