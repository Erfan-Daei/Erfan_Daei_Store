using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries
{
    public interface IGetRolesRepo
    {
        List<IdtRole> GetAllRoles();
    }
}
