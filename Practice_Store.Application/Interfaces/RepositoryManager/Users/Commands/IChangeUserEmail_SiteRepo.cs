using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IChangeUserEmail_SiteRepo
    {
        IdtUser? EmailExist(string Email);

        string GenerateChangeEmailToken(IdtUser User);
    }
}
