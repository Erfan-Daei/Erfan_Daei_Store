using Endpoint.Api.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.UserManagement
{
    [Route("api/UserManager/[controller]")]
    [Authorize]
    [ApiController]
    public class PasswordManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        private readonly IReadToken _readToken;

        public PasswordManagerController(IUserFacad userFacad , IReadToken readToken)
        {
            _userFacad = userFacad;

            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);

            _readToken = readToken;
        }

        [HttpPost]
        public IActionResult POST([FromBody] ChangePasswordDto _Request)
        {
            var UserId = _readToken.GetUserId(User);

            if (_Request.NewPassword != _Request.ConPassword)
            {
                return BadRequest("رمز عبور و تایید آن برابر نیست");
            }

            var Result = _userFacad.ForgetPasswordService.CheckPassword(UserId, _Request.NewPassword);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            string? CallbackUrl = Url.Action(nameof(GET), "ChangePassword", new
            {
                UserId = UserId,
                Token = Result.Token,
                NewPassword = _Request.NewPassword,
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تغییر رمز عبور بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.Email, body, "تغییر رمز عبور");

            string Uri = Url.Action(nameof(GET), "ChangePassword", new { UserId = UserId, Token = Result.Token, NewPassword = _Request.NewPassword }) ?? "";

            dynamic Output = new
            {
                Message = "تاییدیه ایمیل برای شما ارسال گردید",
                StatusCode = StatusCodes.Status202Accepted,
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
                },
            };

            return Accepted(Uri, Output);
        }

        [HttpGet]
        public IActionResult GET([FromQuery] ChangePassword_ConfirmationTokenDto _Request)
        {
            if (string.IsNullOrEmpty(_Request.UserId) || string.IsNullOrEmpty(_Request.Token) || string.IsNullOrEmpty(_Request.NewPassword))
            {
                return BadRequest();
            }

            var Result = _userFacad.ForgetPasswordService.UpdatePassword(_Request.UserId, _Request.Token, _Request.NewPassword);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", Request.Scheme) ?? "",
                        Rel = "Update",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { _Request.UserId }, Request.Scheme) ?? "",
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
    }
}
