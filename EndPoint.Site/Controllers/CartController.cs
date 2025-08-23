using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Services.Carts;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly CookieManager _cookieManager;
        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
            _cookieManager = new CookieManager();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var UserId = ClaimUtility.GetUserId(User);
            return View(_cartServices.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId).Data);
        }

        [HttpPost]
        public IActionResult AddToCart(RequestCartDto Request)
        {
            return Json(_cartServices.AddToCart(new RequestCartDto
            {
                BrowserId = _cookieManager.GetBrowserId(HttpContext),
                Count = Request.Count,
                ProductId = Request.ProductId,
                ProductSizeId = Request.ProductSizeId,
                UserId = ClaimUtility.GetUserId(User),
            }));
        }

        [HttpDelete]
        public IActionResult RemoveFromCart(RequestCartDto Request)
        {
            return Json(_cartServices.RemoveFromCart(new RequestCartDto
            {
                BrowserId = _cookieManager.GetBrowserId(HttpContext),
                Count = Request.Count,
                ProductId = Request.ProductId,
                ProductSizeId = Request.ProductSizeId,
                UserId = ClaimUtility.GetUserId(User),
            }));
        }
    }
}
