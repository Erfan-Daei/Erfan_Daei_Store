using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Services.Carts;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent]
    public class CartNumber : ViewComponent
    {
        private readonly ICartServices _cartServices;
        private readonly CookieManager cookieManager;
        public CartNumber(ICartServices cartServices)
        {
            _cartServices = cartServices;
            cookieManager = new CookieManager();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var CartNumber = _cartServices.GetCart(cookieManager.GetBrowserId(HttpContext), userId).Data;
            return View(viewName: "CartNumber", CartNumber ?? null);
        }
    }
}
