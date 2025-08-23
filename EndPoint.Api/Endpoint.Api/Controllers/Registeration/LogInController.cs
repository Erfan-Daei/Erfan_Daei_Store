using Endpoint.Api.Model.Registeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.JWTToken;
using Practice_Store.Common;
using Practice_Store.Infrastructure.JWTToken;
using System.Data;

namespace Endpoint.Api.Controllers.Registeration
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly IConfiguration _configuration;
        private readonly IReadToken _readToken;
        public LogInController(IUserFacad userFacad,
            IConfiguration configuration,
            IReadToken readToken)
        {
            _userFacad = userFacad;
            _configuration = configuration;
            _readToken = readToken;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult POST([FromBody] LogInDto _Request)
        {
            var Result = _userFacad.LogInUserService.Execute(_Request.Email, _Request.Password);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            var SaveToken = _userFacad.SaveTokenService.SaveToken(Result.Data.UserId, Result.Data.Email, Result.Data.Roles);

            dynamic Output = new
            {
                Message = "حساب با موفقیت ثبت شد . تاییدیه برای ایمیل شما ارسال گردید",
                StatusCode = StatusCodes.Status202Accepted,
                JwtToken = SaveToken.Data.Item1,
                RefreshToken = SaveToken.Data.Item2,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { UserId = "UserId" }, Request.Scheme) ?? "",
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
                        Href = Url.Action("DELETE", "UserManager", new { UserId = "UserId" }, Request.Scheme) ?? "",
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

        [HttpGet]
        [Route("LogOut")]
        public IActionResult GET()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _userFacad.LogOutService.Execute(UserId);

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
            };

            return Ok(Output);
        }

        [HttpGet]
        [Route("RefreshToken")]
        public IActionResult GET([FromHeader] string RefreshToken)
        {
            var Result = _userFacad.RefreshTokenService.Execute(RefreshToken);

            if (!Result.IsSuccess)
                return Problem("توکن کاربر یافت نشد", "", 401);

            dynamic Output = new
            {
                Message = "توکن جدید صادر شد",
                StatusCode = StatusCodes.Status202Accepted,
                JwtToken = Result.Data.Item1,
                RefreshToken = Result.Data.Item2,
            };
            return Ok(Output);
        }
    }
}

//Create Cookie If needed
/*            Response.Cookies.Append("Access_Token", Result.Data.Item1, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:expire"]))
            });

            Response.Cookies.Append("Refresh_Token", Result.Data.Item2, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWTConfig:refreshExpire"]))
            });*/