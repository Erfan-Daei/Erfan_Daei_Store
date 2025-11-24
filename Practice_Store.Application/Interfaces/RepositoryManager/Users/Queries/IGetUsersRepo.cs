using Practice_Store.Application.Services.Users.Queries.GetUsers;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries
{
    public interface IGetUsersRepo
    {
        IQueryable<(string UserId, string? Name)>? SearchRoles(string SearchKey);
        List<IdtUser> GetUsers(RequestGetUsersDto Request, IQueryable<(string UserId, string? Name)>? UserRoles);
    }
}
