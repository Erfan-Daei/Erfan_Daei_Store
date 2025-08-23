using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.EditUser;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace EndPoint.Site.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class UserDetailController : Controller
    {
        private readonly IUserFacad _userFacad;
        private readonly IOrderFacad _orderFacad;
        private readonly EmailSender _emailSender;
        public UserDetailController(IUserFacad userFacad, IOrderFacad orderFacad)
        {
            _userFacad = userFacad;
            _orderFacad = orderFacad;
            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var UserId = ClaimUtility.GetUserId(User);
            return View(_userFacad.GetUserDetail_SiteService.GetUser(UserId).Data);
        }

        [HttpGet]
        public IActionResult GetOrders(string UserId)
        {
            return View(_orderFacad.GetUserOrdersService.Execute(UserId).Data);
        }

        [HttpPatch]
        public IActionResult ChangeOrderState(long OrderId, OrderState OrderState)
        {
            var UserId = ClaimUtility.GetUserId(User);
            return Json(_orderFacad.ChangeOrderState_UserService.Execute(UserId, OrderId, OrderState));
        }

        [HttpGet]
        public IActionResult EditUserdetail()
        {
            var UserId = ClaimUtility.GetUserId(User);
            return View(_userFacad.GetUserDetail_SiteService.GetUser(UserId).Data);
        }

        [HttpPut]
        public IActionResult EditUserdetail(RequestEditUser_SiteDto Request)
        {
            return Json(_userFacad.EditUser_SiteService.EditUser(Request));
        }

        [HttpGet]
        public IActionResult ChangeUserPassword()
        {
            ViewBag.UserEmail = ClaimUtility.GetEmail(User);
            return View();
        }

        [HttpGet]
        public IActionResult ChangeUserEmail()
        {
            ViewBag.UserId = ClaimUtility.GetUserId(User);
            ViewBag.UserEmail = ClaimUtility.GetEmail(User);
            return View();
        }

        [HttpPost]
        public IActionResult ChangeUserEmail(string NewEmail)
        {
            var UserId = ClaimUtility.GetUserId(User);
            var UserEmail = ClaimUtility.GetEmail(User);
            var Result = _userFacad.ChangeUserEmail_SiteService.CheckEmailValidation(UserId, UserEmail, NewEmail);

            string? CallbackUrl = Url.Action("ChangeUserEmailValidation", "UserDetail", new
            {
                UserId = UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای تایید ایمیل بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(NewEmail, body, "تایید ایمیل کاربر");

            return Json(Result);
        }

        [HttpGet]
        public IActionResult ChangeUserEmailValidation(string UserId, string Token)
        {
            return Json(_userFacad.RegisterUserService.ValidateEmail(UserId, Token));
        }

    }
}
