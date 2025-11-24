using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class DeleteUserRepo : IDeleteUserRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        public DeleteUserRepo(UserManager<IdtUser> userManager)
        {
            _userManager = userManager;
        }

        public IdentityResult RemoveFromRole(IdtUser user, IList<string> Roles)
        {
            return _userManager.RemoveFromRolesAsync(user, Roles).Result;
        }
    }
}
