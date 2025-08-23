using EndPoint.Site.Models.ViewModels.CheckOut;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Carts;
using Practice_Store.Application.Services.Orders.Commands.AddOrder;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;
using Practice_Store.Domain.Entities.Orders;
using System.Text;

namespace EndPoint.Site.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class OrderController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly IUserFacad _userFacad;
        private readonly IOrderFacad _orderFacad;
        private readonly CookieManager cookieManager;
        private readonly HttpClient client;

        public OrderController(ICartServices cartServices, IUserFacad userFacad,
            IOrderFacad orderFacad)
        {
            _cartServices = cartServices;
            _userFacad = userFacad;
            _orderFacad = orderFacad;
            cookieManager = new CookieManager();
            client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index(int Shipping)
        {
            var userId = ClaimUtility.GetUserId(User);
            CheckOutViewModel checkOutViewModel = new CheckOutViewModel()
            {
                Cart = _cartServices.GetCart(cookieManager.GetBrowserId(HttpContext), userId).Data,
                UserDetail = _userFacad.GetUserDetail_SiteService.GetUser(userId.ToString()).Data,
                ShippingPrice = Shipping,
            };
            return View(checkOutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequestOrder(RequestAddRequestOrder Request)
        {
            var UserId = ClaimUtility.GetUserId(User);
            var _Cart = _cartServices.GetCart(cookieManager.GetBrowserId(HttpContext), UserId).Data;
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

            //درگاه پرداخت
            var requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
            var jsonContent = JsonConvert.SerializeObject(new
            {
                merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                amount = OrderRequest.Data.TotalSum + Request.Shipping,
                description = $"خرید پوشاک از سایت به شماره",
                callback_url = $"http://localhost:5215/order/ValidateRequestOrder?Guid={OrderRequest.Data.Guid}&Shipping={Request.Shipping}",
            });
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (responseJson.data.authority != null)
            {
                return Json(new { isSuccess = true, data = new { authority = (string)responseJson.data.authority } });
            }
            else
            {
                return Json(new { isSuccess = false, message = "Failed to get authority code from ZarinPal" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ValidateRequestOrder(Guid Guid, int Shipping, string authority, string status)
        {
            var OrderRequest = _orderFacad.GetRequestOrderService.Execute(Guid).Data;
            var requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
            var jsonContent = JsonConvert.SerializeObject(new
            {
                merchant_id = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                amount = OrderRequest.TotalSum + OrderRequest.Shipping,
                authority = authority,
            });
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUrl, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
            int Code = responseJson.data?.code ?? responseJson.errors.code;

            return await AddOrder(new RequestAddOrder
            {
                Authority = authority,
                RefId = responseJson.data?.ref_id ?? 0,
                OrderRequestId = OrderRequest.Id,
            }, Code, Shipping);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(RequestAddOrder Request, int Code, int Shipping)
        {
            if (Code == 100)
            {
                var UserId = ClaimUtility.GetUserId(User);
                _orderFacad.AddOrderService.Execute(new RequestAddOrder
                {
                    CartId = _cartServices.GetCart(cookieManager.GetBrowserId(HttpContext), UserId).Data.Id,
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
