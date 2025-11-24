using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IForgetPasswordRepo
    {
        string GeneratePasswordResetToken(IdtUser user);
        IdentityResult ResetPassword(IdtUser user, string Token, string NewPassword); 
    }
}
