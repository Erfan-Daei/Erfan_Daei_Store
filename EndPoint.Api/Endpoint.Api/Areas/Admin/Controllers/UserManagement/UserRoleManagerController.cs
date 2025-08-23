using Endpoint.Api.Areas.Admin.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.EditUserRole;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.UserManagement
{
    [Route("api/Area/Admin/UserManager/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "UserManagementAdmins")]
    [ApiController]
    public class UserRoleManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        public UserRoleManagerController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        [HttpGet]
        public IActionResult GET(string UserId)
        {
            var Result = _userFacad.GetUserRolesService.GetUserRoles(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                UserRoles = Result.Data,
                Statuscode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = UserId }, Request.Scheme) ?? "",
                        Rel = "UserDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = UserId }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] EditUserRolesDto _Request)
        {
            var Result = _userFacad.EditUserRoleService.AddRoles(new RequestEditUserRole
            {
                UserId = _Request.UserId,
                Roles = _Request.Roles
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                Statuscode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserRoleManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserRoles",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserRoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserRolesDelete",
                        Method = "DELETE"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE([FromBody] EditUserRolesDto _Request)
        {
            var Result = _userFacad.EditUserRoleService.DeleteRoles(new RequestEditUserRole
            {
                UserId = _Request.UserId,
                Roles = _Request.Roles
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                Statuscode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserRoleManager", new { Area = "Admin", UserId = _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "UserRoles",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "UserRoleManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserRolesADD",
                        Method = "POST"
                    },
                },
            };

            return Ok(Output);
        }
    }
}
