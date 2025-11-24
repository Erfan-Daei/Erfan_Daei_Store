using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Queries
{
    public class GetRolesRepo : IGetRolesRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetRolesRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<IdtRole> GetAllRoles()
        {
            return _databaseContext.Roles.ToList();
        }
    }
}
