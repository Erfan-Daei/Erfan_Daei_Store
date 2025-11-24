using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class ConfirmEmailRepo : IConfirmEmailRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public ConfirmEmailRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public bool ConfirmEmail(IdtUser user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token).Result.Succeeded;
        }

        public string GenerateToken(IdtUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
        }
    }
}
