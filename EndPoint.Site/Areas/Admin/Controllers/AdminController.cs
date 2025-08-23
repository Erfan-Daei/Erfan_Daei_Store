using EndPoint.Site.Filters;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
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
            return Json(_userFacad.EditUser_AdminService.EditUser(new RequestEditUser_AdminDto
            {
                UserId = _Request.UserId,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                PhoneNumber = _Request.PhoneNumber,
            }));
        }


        [HttpPost]
        public IActionResult ChangeAdminPasswordValidation(string UserId, string NewPassword, string ConPassword)
        {
            if (NewPassword != ConPassword)
            {
                return BadRequest("رمز عبور و تایید آن برابر نیست");
            }

            var Result = _userFacad.ForgetPasswordService.CheckPassword(UserId, NewPassword);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            string? CallbackUrl = Url.Action("ChangeAdminPassword", "Admin", new
            {
                UserId = UserId,
                Token = Result.Token,
                NewPassword = NewPassword,
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
        public IActionResult ChangeAdminEmail(string PreviousEmail, string NewEmail)
        {

            if (string.IsNullOrEmpty(PreviousEmail) || string.IsNullOrEmpty(NewEmail))
            {
                return BadRequest();
            }
            var UserId = ClaimUtility.GetUserId(HttpContext.User);
            var Result = _userFacad.ChangeUserEmail_SiteService.CheckEmailValidation(UserId, PreviousEmail, NewEmail);
            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            string? CallbackUrl = Url.Action("ChangeAdminEmailValidation", "Admin", new
            {
                UserId = UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(NewEmail, body, "تایید ایمیل کاربر");

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
