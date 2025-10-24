using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager
{
    public interface IManageUserRepository
    {
        IdtUser? FindUserById(string UserId);
        IdtUser? FindUserByEmail(string UserId);
        bool ChangeUserActivation(IdtUser User);
        IdtUser? EmailExist(string Email);
        bool UpdateUser(IdtUser User);
        string GenerateChangeEmailToken(IdtUser User);
    }
}
