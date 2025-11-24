using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager
{
    public interface IUserRepoFinder
    {
        IdtUser? FindUserById(string UserId);
        IdtUser? FindUserByEmail(string UserId);
        IdentityResult UpdateUser(IdtUser User);
        List<string> GetRoles(IdtUser User);
        IdtUser? EmailExist(string email);
    }
}
