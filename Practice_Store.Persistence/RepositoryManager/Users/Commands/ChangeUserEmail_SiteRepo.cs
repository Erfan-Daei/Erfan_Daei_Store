using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class ChangeUserEmail_SiteRepo : IChangeUserEmail_SiteRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public ChangeUserEmail_SiteRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public IdtUser? EmailExist(string Email)
        {
            return _userManager.Users.IgnoreQueryFilters().FirstOrDefault(p => p.Email.ToLower() == Email.ToLower());
        }

        public string GenerateChangeEmailToken(IdtUser User)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(User).Result;
        }
    }
}
