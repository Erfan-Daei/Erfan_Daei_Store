using Endpoint.Api.Model.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.OrderManagement
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Customer&Admin")]
    [ApiController]
    public class OrderManagerController : ControllerBase
    {
        private readonly IOrderFacad _orderFacad;
        private readonly IReadToken _readToken;
        public OrderManagerController(IOrderFacad orderFacad, IReadToken readToken)
        {
            _orderFacad = orderFacad;
            _readToken = readToken;
        }

        [HttpGet]
        public IActionResult GET()
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _orderFacad.GetUserOrdersService.Execute(UserId);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Orders = Result.Data,
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { UserId = UserId }, Request.Scheme) ?? "",
                        Rel = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "OrderManager", Request.Scheme) ?? "",
                        Rel = "UpdateState",
                        Method = "PUT"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] ChangeOrderStateDto_User _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _orderFacad.ChangeOrderState_UserService.Execute(UserId, _Request.OrderId, _Request.OrderState);

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
                        Href = Url.Action("GET", "UserManager", new { UserId = UserId }, Request.Scheme) ?? "",
                        Rel = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "OrderManager", Request.Scheme) ?? "",
                        Rel = "OrderList",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
