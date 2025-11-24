using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Application.Services.Users.Queries.GetUsers;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.RepositoryManager.Users.Queries
{
    public class GetUsersRepo : IGetUsersRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetUsersRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<(string UserId, string? Name)>? SearchRoles(string SearchKey)
        {
            return (IQueryable<(string UserId, string? Name)>?)_databaseContext.UserRoles
                    .AsNoTracking()
                    .Join(_databaseContext.Roles
                    .AsNoTracking(),
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole.UserId, role.Name })
                    .Where(role => role.Name.Contains(SearchKey));
        }

        public List<IdtUser> GetUsers(RequestGetUsersDto Request, IQueryable<(string UserId, string? Name)>? UserRoles)
        {
            return _databaseContext.Users
                    .AsNoTracking()
                    .Where(u => string.IsNullOrEmpty(Request.SearchKey) ||
                    u.Name.Contains(Request.SearchKey) ||
                    u.LastName.Contains(Request.SearchKey) ||
                    u.Email.Contains(Request.SearchKey) ||
                    u.Address.Contains(Request.SearchKey) ||
                    u.PhoneNumber.Contains(Request.SearchKey) ||
                    UserRoles.Any(ur => ur.UserId == u.Id))
                    .ToPaged(Request.Page ?? 1, Request.PageSize ?? 20)
                    .ToList();
        }
    }
}
