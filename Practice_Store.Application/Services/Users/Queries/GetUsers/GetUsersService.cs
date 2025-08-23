using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using System.Data;
using System.Data.SqlTypes;

namespace Practice_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsers
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly IDatabaseContext _databaseContext;
        public GetUsersService(UserManager<IdtUser> userManager, IDatabaseContext databaseContext)
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultGetUsersDTO> GetUsers(RequestGetUsersDto Request)
        {
            try
            {
                var _UserRoles = _databaseContext.UserRoles
                    .AsNoTracking()
                    .Join(_databaseContext.Roles
                    .AsNoTracking(),
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole.UserId, role.Name })
                    .Where(role => role.Name.Contains(Request.SearchKey));

                var _UserList = _databaseContext.Users
                    .AsNoTracking()
                    .Where(u => string.IsNullOrEmpty(Request.SearchKey) ||
                    u.Name.Contains(Request.SearchKey) ||
                    u.LastName.Contains(Request.SearchKey) ||
                    u.Email.Contains(Request.SearchKey) ||
                    u.Address.Contains(Request.SearchKey) ||
                    u.PhoneNumber.Contains(Request.SearchKey) ||
                    _UserRoles.Any(ur => ur.UserId == u.Id))
                    .ToPaged(Request.Page ?? 1, Request.PageSize ?? 20)
                    .Select(user => new IdtUser
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        Address = user.Address,
                        PostCode = user.PostCode,
                        PhoneNumber = user.PhoneNumber,
                        LockoutEnabled = user.LockoutEnabled,
                        EmailConfirmed = user.EmailConfirmed,
                        NormalizedEmail = user.NormalizedEmail
                    }).ToList();


                int RowsCount = _UserList.Count;

                var _UserListWithRole = new List<UserWithRoles>();
                foreach (var user in _UserList)
                {
                    var _Roles = _userManager.GetRolesAsync(user).Result.ToList();

                    _UserListWithRole.Add(new UserWithRoles
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        Address = user.Address,
                        PostCode = user.PostCode,
                        Mobile = user.PhoneNumber,
                        IsActive = user.LockoutEnabled == true ? "غیر فعال" : "فعال",
                        EmailConfirmed = user.EmailConfirmed,
                        Roles = _Roles
                    });
                }

                return new ResultDto<ResultGetUsersDTO>
                {
                    Data = new ResultGetUsersDTO
                    {
                        CurrentPage = Request.Page ?? 1,
                        PageSize = Request.PageSize ?? 20,
                        RowsCount = RowsCount,
                        UsersDtos = _UserListWithRole,
                    },
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (SqlTypeException)
            {
                return new ResultDto<ResultGetUsersDTO>
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}