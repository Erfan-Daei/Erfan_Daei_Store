using Endpoint.Api.Model.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.Cookie;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.Services.Orders.Commands.AddOrder;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;

namespace Endpoint.Api.Controllers.OrderManagement
{
    [Route("api/OrderManager/[controller]")]
    [Authorize(Policy = "Customer&Admin")]
    [ApiController]
    public class OrderRequestManagerController : ControllerBase
    {
        private readonly IOrderFacad _orderFacad;
        private readonly IReadToken _readToken;
        private readonly ICartFacad _cartFacad;
        private readonly IUserFacad _userFacad;
        private readonly IManageCookie _cookieManager;
        public OrderRequestManagerController(IOrderFacad orderFacad,
            IReadToken readToken,
            ICartFacad cartServices,
            IUserFacad userFacad,
            IManageCookie cookieManager)
        {
            _orderFacad = orderFacad;
            _readToken = readToken;
            _cartFacad = cartServices;
            _userFacad = userFacad;
            _cookieManager = cookieManager;
        }

        [HttpGet]
        [Route("GetOrderRequest")]
        public async Task<IActionResult> GET([FromQuery] int Shipping)
        {
            var UserId = _readToken.GetUserId(User);

            var Cart = _cartFacad.GetCartService.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId);
            var UserDetail = _userFacad.GetUserDetail_SiteService.GetUser(UserId);

            if (!UserDetail.IsSuccess)
                return Problem(UserDetail.Message, "", Convert.ToInt16(UserDetail.StatusCode));

            dynamic Output = new
            {
                Cart = Cart.Data,
                UserDetail = UserDetail.Data,
                ShippingPrice = Shipping,
                StatusCode = StatusCodes.Status200OK,
            };

            return Ok(Output);
        }

        [HttpPost]
        [Route("AddOrderRequest")]
        public async Task<IActionResult> POST([FromBody] RequestAddRequestOrderDto _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var _Cart = _cartFacad.GetCartService.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId).Data;

            if (_Cart.CartProducts.Count == 0)
            {
                return StatusCode(404, "سبد شما خالی است");
            }

            var OrderRequest = _orderFacad.AddRequestOrederService.Execute(new RequestAddRequestOrderDto
            {
                UserId = UserId,
                TotalSum = _Cart.TotalSum,
                Shipping = _Request.Shipping,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                Mobile = _Request.Mobile,
            });

            if (!OrderRequest.IsSuccess)
            {
                return Problem(OrderRequest.Message, "", Convert.ToInt16(OrderRequest.StatusCode));
            }

            if (OrderRequest.Data.Authority != null)
            {
                return Ok(new { isSuccess = true, data = new { authority = OrderRequest.Data.Authority } });
            }
            else
            {
                return StatusCode(502, new { isSuccess = false, message = "Failed to get authority code from ZarinPal" });
            }
        }

        [HttpGet]
        [Route("ValidateOrderRequest")]
        public async Task<IActionResult> GET([FromQuery] ValidateOrderRequestDto _Request)
        {
            var OrderRequest = _orderFacad.GetRequestOrderService.Execute(_Request.Guid, _Request.authority).Data;
            return await POST(new AddOrderDto
            {
                Authority = _Request.authority,
                RefId = OrderRequest.RefId,
                OrderRequestId = OrderRequest.Id,
                Code = OrderRequest.Code,
                Shipping = _Request.Shipping
            });
        }

        [HttpPost]
        [Route("AddOrder")]
        private async Task<IActionResult> POST(AddOrderDto _Request)
        {
            if (_Request.Code == 100)
            {
                var UserId = _readToken.GetUserId(User);
                _orderFacad.AddOrderService.Execute(new RequestAddOrder
                {
                    CartId = _cartFacad.GetCartService.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId).Data.Id,
                    UserId = UserId,
                    Authority = _Request.Authority,
                    RefId = _Request.RefId,
                    OrderRequestId = _Request.OrderRequestId,
                });
                return Ok(new { Message = "خرید با موفقیت ثبت شد", RedirectUrl = "api/OrderManager" });
            }
            else
            {
                _orderFacad.UpdateFailedRequestOrderService.Execute(_Request.OrderRequestId, _Request.Authority, _Request.Code);
                return StatusCode(500, new { Message = "عملیات ناموفق!", RedirectUrl = $"api/OrderManager/Shipping={_Request.Shipping}" });
            }
        }
    }
}
