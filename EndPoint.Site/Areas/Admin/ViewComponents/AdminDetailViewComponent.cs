using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Services.Users.Queries.GetAdminDetail;

namespace EndPoint.Site.Areas.Admin.ViewComponents
{
    public class AdminDetailViewComponent : ViewComponent
    {
        private readonly IGetAdminDetail _getAdminDetail;
        public AdminDetailViewComponent(IGetAdminDetail getAdminDetail)
        {
            _getAdminDetail = getAdminDetail;
        }

        public IViewComponentResult Invoke()
        {
            var UserId = ClaimUtility.GetUserId(HttpContext.User);
            var AdminDetail = _getAdminDetail.GetDetail(UserId).Data;
            return View(viewName: "AdminDetail", AdminDetail);
        }
    }
}
