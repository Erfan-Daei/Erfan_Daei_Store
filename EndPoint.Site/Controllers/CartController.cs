using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Carts.Commands;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartFacad _cartFacad;
        private readonly CookieManager _cookieManager;
        public CartController(ICartFacad cartFacad)
        {
            _cartFacad = cartFacad;
            _cookieManager = new CookieManager();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var UserId = ClaimUtility.GetUserId(User);
            return View(_cartFacad.GetCartService.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId).Data);
        }

        [HttpPost]
        public IActionResult AddToCart(RequestCartDto Request)
        {
            return Json(_cartFacad.AddToCartService.AddToCart(new RequestCartDto
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
            return Json(_cartFacad.RemoveFromCartService.RemoveFromCart(new RequestCartDto
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
