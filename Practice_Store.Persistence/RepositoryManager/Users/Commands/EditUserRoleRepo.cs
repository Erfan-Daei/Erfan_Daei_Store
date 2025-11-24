using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class EditUserRoleRepo : IEditUserRoleRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly RoleManager<IdtRole> _roleManager;
        public EditUserRoleRepo(UserManager<IdtUser> userManager, RoleManager<IdtRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IdentityResult AddToRoles(IdtUser user, List<string> roles)
        {
            return _userManager.AddToRolesAsync(user, roles).Result;
        }

        public IdentityResult RemoveFromRoles(IdtUser user, List<string> roles)
        {
            return _userManager.RemoveFromRolesAsync(user, roles).Result;
        }

        public bool RoleExist(string role)
        {
            return _roleManager.RoleExistsAsync(role).Result;
        }
    }
}
