using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IEditUser_AdminRepo
    {
        IdtUser? EmailExist(string email);
        IdentityResult ResetPassword(IdtUser user, string Password);
    }
}
