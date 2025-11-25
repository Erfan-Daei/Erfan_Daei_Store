using EndPoint.Site.Models.ViewModels.CheckOut;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Orders.Commands.AddOrder;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;

namespace EndPoint.Site.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class OrderController : Controller
    {
        private readonly ICartFacad _cartFacad;
        private readonly IUserFacad _userFacad;
        private readonly IOrderFacad _orderFacad;
        private readonly CookieManager cookieManager;

        public OrderController(ICartFacad cartFacad, IUserFacad userFacad,
            IOrderFacad orderFacad)
        {
            _cartFacad = cartFacad;
            _userFacad = userFacad;
            _orderFacad = orderFacad;
            cookieManager = new CookieManager();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int Shipping)
        {
            var userId = ClaimUtility.GetUserId(User);
            CheckOutViewModel checkOutViewModel = new CheckOutViewModel()
            {
                Cart = _cartFacad.GetCartService.GetCart(cookieManager.GetBrowserId(HttpContext), userId).Data,
                UserDetail = _userFacad.GetUserDetail_SiteService.GetUser(userId.ToString()).Data,
                ShippingPrice = Shipping,
            };
            return View(checkOutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestOrder(RequestAddRequestOrder Request)
        {
            var UserId = ClaimUtility.GetUserId(User);
            var _Cart = _cartFacad.GetCartService.GetCart(cookieManager.GetBrowserId(HttpContext), UserId).Data;
            if (_Cart.CartProducts.Count == 0)
            {
                return Json(new { message = "سبد شما خالی است" });
            }
            var OrderRequest = _orderFacad.AddRequestOrederService.Execute(new RequestAddRequestOrder
            {
                UserId = UserId,
                TotalSum = _Cart.TotalSum,
                Shipping = Request.Shipping,
                Name = Request.Name,
                LastName = Request.LastName,
                Address = Request.Address,
                PostCode = Request.PostCode,
                Mobile = Request.Mobile,
            });
            if (!OrderRequest.IsSuccess)
            {
                return Json(OrderRequest);
            }

            if (OrderRequest.Data.Authority != null)
            {
                return Json(new { isSuccess = true, data = new { authority = (string)OrderRequest.Data.Authority } });
            }
            else
            {
                return Json(new { isSuccess = false, message = "Failed to get authority code from ZarinPal" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ValidateRequestOrder(Guid Guid, int Shipping, string authority, string status)
        {
            var OrderRequest = _orderFacad.GetRequestOrderService.Execute(Guid, authority).Data;

            return await AddOrder(new RequestAddOrder
            {
                Authority = authority,
                RefId = OrderRequest.RefId,
                OrderRequestId = OrderRequest.Id,
            }, OrderRequest.Code, Shipping);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(RequestAddOrder Request, int Code, int Shipping)
        {
            if (Code == 100)
            {
                var UserId = ClaimUtility.GetUserId(User);
                _orderFacad.AddOrderService.Execute(new RequestAddOrder
                {
                    CartId = _cartFacad.GetCartService.GetCart(cookieManager.GetBrowserId(HttpContext), UserId).Data.Id,
                    UserId = UserId,
                    Authority = Request.Authority,
                    RefId = Request.RefId,
                    OrderRequestId = Request.OrderRequestId,
                });
                return Redirect($"/userdetail/getorders?userid={UserId}");
            }
            else
            {
                _orderFacad.UpdateFailedRequestOrderService.Execute(Request.OrderRequestId, Request.Authority, Code);
                return Redirect($"/order/index?Shipping={Shipping}");
            }
        }
    }
}
