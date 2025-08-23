using Endpoint.Api.Model.Registeration;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.Registeration
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        public SignUpController(IUserFacad userFacad)
        {
            _userFacad = userFacad;

            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpPost]
        public IActionResult POST([FromBody] SignUpDto _Request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Result = _userFacad.RegisterUserService.ValidateUser(new RequestRegisterUserDto
            {
                Name = _Request.Name,
                LastName = _Request.LastName,
                Email = _Request.Email,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                Password = _Request.Password,
                ConPassword = _Request.ConPassword,
            });

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            string? CallbackUrl = Url.Action("GET", "EmailManager", new
            {
                UserId = Result.UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.UserEmail, body, "فعال سازی حساب کاربری");

            string Uri = Url.Action("GET", "EmailManager", new { UserId = Result.UserId, Token = Result.EmailValidationToken }) ?? "";

            dynamic Output = new
            {
                Message = "حساب با موفقیت ثبت شد . تاییدیه برای ایمیل شما ارسال گردید",
                StatusCode = StatusCodes.Status202Accepted,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { UserId = Result.UserId }, Request.Scheme) ?? "",
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
                        Href = Url.Action("DELETE", "UserManager", new { UserId = Result.UserId }, Request.Scheme) ?? "",
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

            return Accepted(Uri, Output);
        }
    }
}
