using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;
using System.Data;
using System.Data.SqlTypes;

namespace Practice_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsers
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IGetUsersRepo _getUsersRepo;
        public GetUsersService(IUserRepoFinder userRepoFinder,
            IGetUsersRepo getUsersRepo)
        {
            _userRepoFinder = userRepoFinder;
            _getUsersRepo = getUsersRepo;
        }

        public ResultDto<ResultGetUsersDTO> GetUsers(RequestGetUsersDto Request)
        {
            try
            {
                var _UserRoles = _getUsersRepo.SearchRoles(Request.SearchKey ?? "");

                var _UserList = _getUsersRepo.GetUsers(Request, _UserRoles)
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
                    var _Roles = _userRepoFinder.GetRoles(user);

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