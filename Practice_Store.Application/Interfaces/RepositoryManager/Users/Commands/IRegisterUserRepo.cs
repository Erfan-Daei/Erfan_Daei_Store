using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IRegisterUserRepo
    {
        IdtRole? FindRole(string roleName);
        IdentityResult CreateUser(IdtUser user, string Password);
        IdentityResult ActivateUser(IdtUser user);
        IdentityResult AddToRole(IdtUser user, List<string> Roles);
        IdentityResult DeleteUser(IdtUser user);
        string GenerateEmailConfirmationToken(IdtUser user);
        IdentityResult ConfirmEmail(IdtUser user, string Token);
    }
}
