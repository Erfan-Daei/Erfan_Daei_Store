using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users
{
    public class UserRepoFinder : IUserRepoFinder
    {
        private readonly UserManager<IdtUser> _userManager;
        public UserRepoFinder(UserManager<IdtUser> userManager)
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

        public IdentityResult UpdateUser(IdtUser User)
        {
            return _userManager.UpdateAsync(User).Result;
        }

        public List<string> GetRoles(IdtUser User)
        {
            return _userManager.GetRolesAsync(User).Result.ToList();
        }

        public IdtUser? EmailExist(string email)
        {
            return _userManager.Users
                .IgnoreQueryFilters()
                .Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
        }
    }
}
