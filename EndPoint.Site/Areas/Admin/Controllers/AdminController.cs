using EndPoint.Site.Filters;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
using Practice_Store.Application.Services.Users.Commands.ForgetPassword;
using Practice_Store.Common;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [RolesExceptCustomerAttribute]
    public class AdminController : Controller
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        public AdminController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var UserId = ClaimUtility.GetUserId(HttpContext.User);
            return View(_userFacad.GetAdminDetailService.GetDetail(UserId).Data);
        }

        [HttpGet]
        public IActionResult EditAdmindetail()
        {
            var UserId = ClaimUtility.GetUserId(User);
            return View(_userFacad.GetAdminDetailService.GetDetail(UserId).Data);
        }

        [HttpPut]
        public IActionResult EditAdmindetail(RequestEditUser_AdminDto _Request)
        {
            return Json(_userFacad.EditUser_AdminService.EditUser(_Request));
        }


        [HttpPost]
        public IActionResult ChangeAdminPasswordValidation(RequestForgetPasswordDto _Request)
        {
            var Result = _userFacad.ForgetPasswordService.CheckPassword(_Request);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            string? CallbackUrl = Url.Action("ChangeAdminPassword", "Admin", new
            {
                UserId = _Request.UserId,
                Token = Result.Token,
                NewPassword = _Request.NewPassword,
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تغییر رمز عبور بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.Email, body, "تغییر رمز عبور");

            return Json(Result);
        }

        [HttpGet]
        public IActionResult ChangeAdminPassword(string UserId, string Token, string NewPassword)
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Token) || string.IsNullOrEmpty(NewPassword))
            {
                return BadRequest();
            }

            var Result = _userFacad.ForgetPasswordService.UpdatePassword(UserId, Token, NewPassword);
            if (!Result.IsSuccess)
                return BadRequest();

            return RedirectToAction("LogOut");
        }

        [HttpPost]
        public IActionResult ChangeAdminEmail(RequestChangeUserEmail_SiteDto _Request)
        {
            var UserId = ClaimUtility.GetUserId(HttpContext.User);
            var LastEmail = ClaimUtility.GetEmail(HttpContext.User);
            var Result = _userFacad.ChangeUserEmail_SiteService.CheckEmailValidation(new RequestChangeUserEmail_SiteDto
            {
                NewEmail = _Request.NewEmail,
                LastEmail = LastEmail,
                UserId = UserId
            });
            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            string? CallbackUrl = Url.Action("ChangeAdminEmailValidation", "Admin", new
            {
                UserId = UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(_Request.NewEmail, body, "تایید ایمیل کاربر");

            return Json(Result);
        }

        [HttpGet]
        public IActionResult ChangeAdminEmailValidation(string UserId, string Token)
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Token))
                return BadRequest();
            var Result = _userFacad.RegisterUserService.ValidateEmail(UserId, Token);
            if (!Result.IsSuccess)
                return BadRequest();
            return RedirectToAction("LogOut");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("LogIn", "Register", new { area = "" });
        }
    }
}
