using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IRefreshTokenRepo
    {
        IdtUsertokens? ChechRefreshToken(string refreshToken);
        bool RemovePreviousToken(string UserId);
        bool AddToken(IdtUsertokens Token);
    }
}
