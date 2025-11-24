using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IDeleteUserRepo
    {
        IList<string>? GetRoles(IdtUser user);
        IdentityResult RemoveFromRole(IdtUser user, IList<string> Roles);
    }
}
