using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "UserManagementAdmins")]
    [ApiController]
    public class UserActivationManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        public UserActivationManagerController(IUserFacad userFacad)
        {
            _userFacad = userFacad;

            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpPut]
        public IActionResult PUT(string UserId)
        {
            var Result = _userFacad.ActivationUserService.ChangeActivationState(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            string? CallbackUrl = Url.Action("GET", "LogIn", null, protocol: Request.Scheme);

            string body = $"حساب کاربری شما به {Result.Data.UserState} تغییر یافت!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.Data.UserEmail, body, "فعال سازی حساب کاربری");

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
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
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserList",
                        Method = "GET"
                    },
                },
            };

            return Ok(Output);
        }
    }
}
