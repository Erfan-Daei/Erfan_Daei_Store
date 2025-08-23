using Endpoint.Api.Areas.Admin.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Queries.RoleManagement;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.UserManagement
{
    [Route("api/Area/Admin/UserManager/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "UserManagementAdmins")]
    [ApiController]
    public class RoleManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        public RoleManagerController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        [HttpGet]
        public IActionResult GET(string? SearchKey, int? Page, int? PageSize)
        {
            var Result = _userFacad.RoleManagementService.GetRoles(new RequestRoleManagement_GetRolesDto
            {
                SearchKey = SearchKey,
                PageSize = PageSize,
                Page = Page
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, SearchKey, Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Roles = Result.Data.Roles,
                Page = Result.Data.Page,
                PageSize = Result.Data.PageSize,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "RoleManager", new { Area = "Admin", RoleName = "RoleName" }, Request.Scheme) ?? "",
                        Rel = "RoleDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewRole",
                        Method = "POST"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpGet("Name")]
        public IActionResult GET(string RoleName)
        {
            var Result = _userFacad.RoleManagementService.GetRoleDetail(RoleName);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Role = Result.Data,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleDelete",
                        Method = "DELETE"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] string RoleName)
        {
            var Result = _userFacad.RoleManagementService.AddRole(RoleName);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "RoleManager", new { Area = "Admin", RoleName = RoleName }, Request.Scheme) ?? "",
                        Rel = "RoleDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleDelete",
                        Method = "DELETE"
                    },
                },

            };

            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] EditRoleDto _Request)
        {
            var Result = _userFacad.RoleManagementService.EditRole(_Request.RoleName, _Request.NewRoleName);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "RoleManager", new { Area = "Admin", RoleName = _Request.NewRoleName }, Request.Scheme) ?? "",
                        Rel = "RoleDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleDelete",
                        Method = "DELETE"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE([FromBody] string RoleName)
        {
            var Result = _userFacad.RoleManagementService.DeleteRole(RoleName);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "RoleList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "RoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewRole",
                        Method = "POST"
                    },
                },
            };

            return Ok(Output);
        }
    }
}
