using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager
{
    public class ManageUserRepository : IManageUserRepository
    {
        private readonly UserManager<IdtUser> _userManager;
        public ManageUserRepository(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }
        public IdtUser? FindUserById(string UserId)
        {
            return _userManager.FindByIdAsync(UserId).Result;
        }
        public IdtUser? FindUserByEmail(string UserEmail)
        {
            return _userManager.FindByEmailAsync(UserEmail).Result;
        }

        public bool ChangeUserActivation(IdtUser User)
        {
            var Result = _userManager.SetLockoutEnabledAsync(User, !User.LockoutEnabled).Result;
            return Result.Succeeded;
        }

        public IdtUser? EmailExist(string Email)
        {
            return _userManager.Users.IgnoreQueryFilters().FirstOrDefault(p => p.Email.ToLower() == Email.ToLower());
        }

        public bool UpdateUser(IdtUser User)
        {
            return _userManager.UpdateAsync(User).Result.Succeeded;
        }

        public string GenerateChangeEmailToken(IdtUser User)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(User).Result;
        }
    }
}
