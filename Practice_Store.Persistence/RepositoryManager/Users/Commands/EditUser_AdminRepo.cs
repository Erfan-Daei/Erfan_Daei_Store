using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class EditUser_AdminRepo : IEditUser_AdminRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public EditUser_AdminRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public IdtUser? EmailExist(string email)
        {
            return _userManager.Users.IgnoreQueryFilters().Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }
        public IdentityResult ResetPassword(IdtUser user, string Password)
        {
            var ResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            return _userManager.ResetPasswordAsync(user, ResetToken, Password).Result;
        }
    }
}
