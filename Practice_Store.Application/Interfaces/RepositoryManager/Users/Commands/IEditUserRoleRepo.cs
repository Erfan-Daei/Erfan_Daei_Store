using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IEditUserRoleRepo
    {
        bool RoleExist(string role);
        IdentityResult AddToRoles(IdtUser user, List<string> roles);
        IdentityResult RemoveFromRoles(IdtUser user, List<string> roles);
    }
}
