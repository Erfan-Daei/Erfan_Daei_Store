using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRoles
    {
        private readonly IDatabaseContext _databaseContext;
        public GetRolesService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            var _Roles = _databaseContext.Roles.ToList().Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = _Roles,
                IsSuccess = true,
                Status_Code = Status_Code.OK,
            };
        }
    }
}
