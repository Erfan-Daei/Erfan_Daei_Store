using Endpoint.Api.Areas.Admin.Model.AdminManagement;
using Endpoint.Api.Areas.Admin.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.Services.Users.Commands.EditUser;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.AdminManagement
{
    [Route("api/Area/Admin/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "Admin_Only")]
    [ApiController]
    public class AdminManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly IReadToken _readToken;

        public AdminManagerController(IUserFacad userFacad, IReadToken readToken)
        {
            _userFacad = userFacad;
            _readToken = readToken;
        }

        [HttpGet]
        public IActionResult GET()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.GetAdminDetailService.GetDetail(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                UserDetail = new Admin_UserDetailDto
                {
                    Id = UserId,
                    Email = Result.Data.Email,
                    EmailConfirmed = Result.Data.EmailConfirmed,
                    Name = Result.Data.Name,
                    LastName = Result.Data.LastName,
                    Address = Result.Data.Address,
                    PostCode = Result.Data.PostCode,
                    Mobile = Result.Data.Mobile,
                    MobileConfirmed = Result.Data.MobileConfirmed,
                    IsActive = Result.Data.IsActive,
                    Roles = Result.Data.Roles

                },
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("PUT", "AdminManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "AdminUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "ChangePassword",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "EmailManager", Request.Scheme) ?? "",
                        Rel = "ChangeEmail",
                        Method = "POST"
                    },
                },
            };
            return Ok(Output);

        }

        [HttpPut]
        public IActionResult PUT([FromBody] EditAdminDto _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.EditUser_SiteService.EditUser(new RequestEditUser_SiteDto
            {
                UserId = UserId,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                Mobile = _Request.Mobile,
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "AdminManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "AdminDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "ChangePassword",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "EmailManager", Request.Scheme) ?? "",
                        Rel = "ChangeEmail",
                        Method = "POST"
                    },
                },
            };
            return Ok(Output);
        }
    }
}
