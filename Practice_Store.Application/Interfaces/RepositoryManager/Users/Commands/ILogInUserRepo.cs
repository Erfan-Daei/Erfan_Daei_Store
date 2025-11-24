using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface ILogInUserRepo
    {
        bool CheckPassword(IdtUser user, string password);
        bool DeletePreviousTokens(string UserId);
        void Save();
    }
}
