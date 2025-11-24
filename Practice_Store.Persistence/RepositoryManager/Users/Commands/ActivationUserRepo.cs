using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class ActivationUserRepo : IActivationUserRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public ActivationUserRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public bool ChangeUserActivation(IdtUser User)
        {
            var Result = _userManager.SetLockoutEnabledAsync(User, !User.LockoutEnabled).Result;
            return Result.Succeeded;
        }
    }
}
