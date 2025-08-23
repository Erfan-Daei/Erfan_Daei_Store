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
    public class EmailManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        private readonly IReadToken _readToken;

        public EmailManagerController(IUserFacad userFacad, IReadToken readToken)
        {
            _userFacad = userFacad;

            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);

            _readToken = readToken;
        }

        [HttpPost]
        public IActionResult POST([FromBody] UpdateEmailDto _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var Email = _readToken.GetUserEmail(User);

            if (string.IsNullOrEmpty(_Request.NewEmail))
            {
                return BadRequest();
            }

            var Result = _userFacad.ChangeUserEmail_SiteService.CheckEmailValidation(UserId, Email, _Request.NewEmail);
            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            string? CallbackUrl = Url.Action(nameof(GET), "EmailManager", new
            {
                UserId = UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(_Request.NewEmail, body, "تایید ایمیل کاربر");

            string Uri = Url.Action(nameof(GET), "EmailManager", new { UserId = UserId, Token = Result.EmailValidationToken }) ?? "";

            dynamic Output = new
            {
                Message = "تاییدیه برای ایمیل شما ارسال گردید",
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
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "Update_Password",
                        Method = "POST"
                    },
                },
            };

            return Accepted(Uri, Output);
        }

        [HttpGet]
        public IActionResult GET([FromQuery] UpdateEmail_ConfirmationTokenDto _Request)
        {
            if (string.IsNullOrEmpty(_Request.UserId) || string.IsNullOrEmpty(_Request.Token))
                return BadRequest();

            var Result = _userFacad.RegisterUserService.ValidateEmail(_Request.UserId, _Request.Token);
            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = "ایمیل شما تایید شد",
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Result.UserId }, Request.Scheme) ?? "",
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
                        Href = Url.Action("DELETE", "UserManager", new { Result.UserId }, Request.Scheme) ?? "",
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

        [HttpPost]
        [Route("EmailConfirmation")]
        public IActionResult POST()
        {
            var UserId = _readToken.GetUserId(User);
            var Email = _readToken.GetUserEmail(User);

            var Result = _userFacad.ConfirmEmailService.GenerateToken(UserId);

            if (!Result.IsSuccess)
                return StatusCode(Convert.ToInt16(Result.StatusCode), Result.Message);

            string? CallbackUrl = Url.Action("EmailConfirmation", "EmailManager", new
            {
                UserId = UserId,
                Token = Result.Data
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Email, body, "تایید ایمیل کاربر");

            string Uri = Url.Action("EmailConfirmation", "EmailManager", new { UserId = UserId, Token = Result.Data }) ?? "";

            dynamic Output = new
            {
                Message = "تاییدیه برای ایمیل شما ارسال گردید",
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
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "Update_Password",
                        Method = "POST"
                    },
                },
            };

            return Accepted(Uri, Output);
        }

        [HttpGet]
        [Route("EmailConfirmation")]
        public IActionResult GET([FromQuery] ConfirmEmail_ConfirmationToken _Request)
        {
            if (string.IsNullOrEmpty(_Request.UserId) || string.IsNullOrEmpty(_Request.Token))
                return BadRequest();

            var Result = _userFacad.ConfirmEmailService.ConfirmEmail(_Request.UserId, _Request.Token);
            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = "ایمیل شما تایید شد",
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
