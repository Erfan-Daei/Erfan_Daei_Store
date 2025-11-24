using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IConfirmEmailRepo
    {
        bool ConfirmEmail(IdtUser user, string token);
        string GenerateToken(IdtUser user);
    }
}
