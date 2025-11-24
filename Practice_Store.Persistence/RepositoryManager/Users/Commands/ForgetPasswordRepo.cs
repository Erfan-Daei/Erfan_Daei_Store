using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class ForgetPasswordRepo : IForgetPasswordRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public ForgetPasswordRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public string GeneratePasswordResetToken(IdtUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user).Result;
        }

        public IdentityResult ResetPassword(IdtUser user, string Token, string NewPassword)
        {
            return _userManager.ResetPasswordAsync(user, Token, NewPassword).Result;
        }
    }
}
