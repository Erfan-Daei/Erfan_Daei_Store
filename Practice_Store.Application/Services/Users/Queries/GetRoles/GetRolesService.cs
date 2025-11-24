using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRoles
    {
        private readonly IGetRolesRepo _getRolesRepo;
        public GetRolesService(IGetRolesRepo getRolesRepo)
        {
            _getRolesRepo = getRolesRepo;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            var _Roles = _getRolesRepo.GetAllRoles()
                .Select(p => new RolesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = _Roles,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
