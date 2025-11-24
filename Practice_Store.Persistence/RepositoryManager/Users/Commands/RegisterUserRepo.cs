using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class RegisterUserRepo : IRegisterUserRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly RoleManager<IdtRole> _roleManager;
        public RegisterUserRepo(UserManager<IdtUser> userManager, RoleManager<IdtRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IdtRole? FindRole(string roleName)
        {
            return _roleManager.FindByNameAsync(roleName).Result;
        }

        public IdentityResult CreateUser(IdtUser user, string Password)
        {
            return _userManager.CreateAsync(user, Password).Result;
        }

        public IdentityResult ActivateUser(IdtUser user)
        {
            return _userManager.SetLockoutEnabledAsync(user, false).Result;
        }

        public IdentityResult AddToRole(IdtUser user, List<string> Roles)
        {
            return _userManager.AddToRolesAsync(user, Roles).Result;
        }

        public IdentityResult DeleteUser(IdtUser user)
        {
            return _userManager.DeleteAsync(user).Result;
        }

        public string GenerateEmailConfirmationToken(IdtUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
        }

        public IdentityResult ConfirmEmail(IdtUser user, string Token)
        {
            return _userManager.ConfirmEmailAsync(user, Token).Result;
        }
    }
}
