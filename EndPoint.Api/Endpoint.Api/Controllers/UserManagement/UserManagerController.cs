using Endpoint.Api.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.Services.Users.Commands.EditUser;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Customer&Admin")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly IReadToken _readToken;
        public UserManagerController(IUserFacad userFacad, IReadToken readToken)
        {
            _userFacad = userFacad;
            _readToken = readToken;
        }

        [HttpGet]
        public IActionResult GET()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.GetUserDetail_SiteService.GetUser(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                UserDetail = new UserDetailDto
                {
                    Id = UserId,
                    Name = Result.Data.Name,
                    LastName = Result.Data.LastName,
                    Address = Result.Data.Address,
                    PostCode = Result.Data.PostCode,
                    Mobile = Result.Data.Mobile,
                    Email = Result.Data.Email,
                },
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", Request.Scheme) ?? "",
                        Rel = "Update",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { UserId }, Request.Scheme) ?? "",
                        Rel = "Delete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "EmailManager", Request.Scheme) ?? "",
                        Rel = "Update_Email",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "Update_Password",
                        Method = "POST"
                    },
                },
            };
            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] EditUserDto _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.EditUser_SiteService.EditUser(new RequestEditUser_SiteDto
            {
                UserId = UserId,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request?.PostCode,
                Mobile = _Request.PhoneNumber,
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { UserId }, Request.Scheme) ?? "",
                        Rel = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { UserId }, Request.Scheme) ?? "",
                        Rel = "Delete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "EmailManager", Request.Scheme) ?? "",
                        Rel = "Update_Email",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "Update_Password",
                        Method = "POST"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.DeleteUserService.DeleteUser(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = "حساب شما با موفقیت حذف گردید",
                StatusCode = StatusCodes.Status200OK,
            };
            return Ok(Output);
        }
    }
}
