using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Common;
using System.Security.Claims;

namespace EndPoint.Site.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        public RegisterController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUpValiadation(RequestRegisterUserDto _Request)
        {
            var Result = _userFacad.RegisterUserService.ValidateUser(_Request);

            string? CallbackUrl = Url.Action("SignUp", "Register", new
            {
                UserId = Result.UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(_Request.Email, body, "تایید ایمیل کاربر");

            return Json(Result);
        }

        [HttpGet]
        public IActionResult SignUp(string UserId, string Token)
        {
            return Json(_userFacad.RegisterUserService.ValidateEmail(UserId, Token));
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string Email, string Password)
        {
            var LogInResult = _userFacad.LogInUserService.Execute(Email, Password);
            if (LogInResult.IsSuccess)
            {
                var _Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, LogInResult.Data.UserId),
                    new Claim(ClaimTypes.Name, LogInResult.Data.FullName),
                    new Claim(ClaimTypes.Email, Email),
                };
                foreach (var item in LogInResult.Data.Roles)
                {
                    _Claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var identity = new ClaimsIdentity(_Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(7)
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(LogInResult);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("LogIn");
        }

        [HttpPost]
        public IActionResult ForgetPasswordValidation(string NewPassword, string ConPassword)
        {
            var UserId = ClaimUtility.GetUserId(User);
            if (NewPassword != ConPassword)
            {
                return Json("رمز عبور و تکرار آن برابر نیست");
            }

            var Result = _userFacad.ForgetPasswordService.CheckPassword(UserId, NewPassword);

            if (!Result.IsSuccess)
            {
                return Json(Result.Message);
            }
            string? CallbackUrl = Url.Action("ForgetPassword", "Register", new
            {
                UserId = UserId,
                Token = Result.Token,
                NewPassword = NewPassword
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.Email, body, "تایید ایمیل کاربر");

            return Json(Result);
        }

        [HttpGet]
        public IActionResult ForgetPassword(string UserId, string Token, string NewPassword)
        {
            return Json(_userFacad.ForgetPasswordService.UpdatePassword(UserId, Token, NewPassword));
        }

        [HttpGet]
        public IActionResult ChooseTemplate()
        {
            if (ClaimUtility.GetRoles(User).Where(p => p.EndsWith("Admin")).Count() >= 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
