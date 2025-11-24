using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public class RoleManagementService : IRoleManagement
    {
        private readonly IRoleManagementRepo _roleManagementRepo;
        public RoleManagementService(IRoleManagementRepo roleManagementRepo)
        {
            _roleManagementRepo = roleManagementRepo;
        }

        public ResultDto AddRole(string RoleName)
        {
            var _Role = new IdtRole
            {
                InsertTime = DateTime.Now,
                Name = RoleName,
            };
            var _AddRole = _roleManagementRepo.CreateRole(_Role);

            if (!_AddRole.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات ناموفق",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "نقش جدید اضافه شد",
                StatusCode = StatusCodes.Status201Created,
            };
        }

        public ResultDto DeleteRole(string RoleName)
        {
            var _Role = _roleManagementRepo.FindByName(RoleName);

            if (_Role == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نقش یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var _Delete = _roleManagementRepo.DeleteRole(_Role);

            if (!_Delete.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "نقش حذف شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public ResultDto EditRole(string RoleName, string NewRoleName)
        {
            var _Role = _roleManagementRepo.FindByName(RoleName);

            if (_Role == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نقش یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            _Role.Name = NewRoleName;
            _Role.NormalizedName = NewRoleName.ToUpper();
            _Role.UpdateTime = DateTime.UtcNow;

            var Update = _roleManagementRepo.UpdateRole(_Role);

            if (!Update.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "نقش با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public ResultDto<RoleManagement_RoleDto> GetRoleDetail(string RoleName)
        {
            var _Role = _roleManagementRepo.FindByName(RoleName);

            if (_Role == null)
            {
                return new ResultDto<RoleManagement_RoleDto>()
                {
                    IsSuccess = false,
                    Message = "نقش یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new ResultDto<RoleManagement_RoleDto>()
            {
                Data = new RoleManagement_RoleDto
                {
                    Name = _Role.Name,
                    RoleId = _Role.Id,
                    UpdateTime = _Role.UpdateTime,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public ResultDto<ResultRoleManagement_GetRolesDto> GetRoles(RequestRoleManagement_GetRolesDto Request)
        {
            var _Roles = _roleManagementRepo.SearchRoles(Request)
                .Select(r => new RoleManagement_RoleDto
                {
                    RoleId = r.Id,
                    Name = r.Name,
                    UpdateTime = r.UpdateTime
                }).ToList();

            return new ResultDto<ResultRoleManagement_GetRolesDto>
            {
                Data = new ResultRoleManagement_GetRolesDto
                {
                    Roles = _Roles,
                    Page = Request.Page ?? 1,
                    PageSize = Request.PageSize ?? 20
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
