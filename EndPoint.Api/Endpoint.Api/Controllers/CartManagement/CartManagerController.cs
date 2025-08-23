using Endpoint.Api.Model.CartManagement;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.Cookie;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.Services.Carts;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.CartManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartManagerController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        private readonly IManageCookie _cookieManager;
        private readonly IReadToken _readToken;
        public CartManagerController(ICartServices cartServices,
            IManageCookie manageCookie,
            IReadToken readToken)
        {
            _cartServices = cartServices;
            _cookieManager = manageCookie;
            _readToken = readToken;
        }

        [HttpGet]
        public IActionResult GET()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _cartServices.GetCart(_cookieManager.GetBrowserId(HttpContext), UserId);

            dynamic Output = new
            {
                Cart = Result.Data,
                StatusCodes = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("POST", "CartManager", Request.Scheme) ?? "",
                        Rel = "AddToCart",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "CartManager", Request.Scheme) ?? "",
                        Rel = "DeleteFromCart",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] CartManagerDto _Request)
        {
            string? UserId = _readToken.GetUserId(User);
            var Result = _cartServices.AddToCart(new RequestCartDto
            {
                BrowserId = _cookieManager.GetBrowserId(HttpContext),
                UserId = UserId,
                Count = _Request.Count,
                ProductId = _Request.ProductId,
                ProductSizeId = _Request.ProductSizeId,
            });

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "CartManager", Request.Scheme) ?? "",
                        Rel = "Getcart",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "CartManager", Request.Scheme) ?? "",
                        Rel = "DeleteFromCart",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE([FromBody] CartManagerDto _Request)
        {
            string? UserId = _readToken.GetUserId(User);
            var Result = _cartServices.RemoveFromCart(new RequestCartDto
            {
                BrowserId = _cookieManager.GetBrowserId(HttpContext),
                UserId = UserId,
                Count = _Request.Count,
                ProductId = _Request.ProductId,
                ProductSizeId = _Request.ProductSizeId,
            });

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("POST", "CartManager", Request.Scheme) ?? "",
                        Rel = "AddToCart",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "CartManager", Request.Scheme) ?? "",
                        Rel = "GetCart",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
