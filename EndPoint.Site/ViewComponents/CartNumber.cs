using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent]
    public class CartNumber : ViewComponent
    {
        private readonly ICartFacad _cartFacad;
        private readonly CookieManager cookieManager;
        public CartNumber(ICartFacad cartFacad)
        {
            _cartFacad = cartFacad;
            cookieManager = new CookieManager();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var CartNumber = _cartFacad.GetCartService.GetCart(cookieManager.GetBrowserId(HttpContext), userId).Data;
            return View(viewName: "CartNumber", CartNumber ?? null);
        }
    }
}
