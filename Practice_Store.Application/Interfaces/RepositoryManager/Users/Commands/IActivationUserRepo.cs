using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface IActivationUserRepo
    {
        bool ChangeUserActivation(IdtUser User);
    }
}
