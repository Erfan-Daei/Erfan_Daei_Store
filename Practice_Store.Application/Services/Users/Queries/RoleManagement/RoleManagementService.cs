using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public class RoleManagementService : IRoleManagement
    {
        private readonly RoleManager<IdtRole> _roleManager;
        public RoleManagementService(RoleManager<IdtRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public ResultDto AddRole(string RoleName)
        {
            var _Role = new IdtRole
            {
                InsertTime = DateTime.Now,
                Name = RoleName,
            };
            var _AddRole = _roleManager.CreateAsync(_Role).Result;

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
            var _Role = _roleManager.FindByNameAsync(RoleName).Result;

            if (_Role == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "نقش یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var _Delete = _roleManager.DeleteAsync(_Role).Result;

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
            var _Role = _roleManager.FindByNameAsync(RoleName).Result;

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

            var Update = _roleManager.UpdateAsync(_Role).Result;

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
            var _Role = _roleManager.FindByNameAsync(RoleName).Result;

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
            var _Roles = _roleManager.Roles
                .Where(r => string.IsNullOrEmpty(Request.SearchKey) ||
                r.Name.Contains(Request.SearchKey) ||
                r.NormalizedName.Contains(Request.SearchKey))
                .ToPaged(Request.Page ?? 1, Request.PageSize ?? 20)
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
