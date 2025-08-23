using Endpoint.Api.Areas.Admin.Model.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin;
using Practice_Store.Common;
using System.ComponentModel;

namespace Endpoint.Api.Areas.Admin.Controllers.OrderManagement
{
    [Route("api/Area/Admin/[controller]")]
    [Authorize(Policy = "OrderManagementAdmins")]
    [ApiController]
    public class OrderManagerController : ControllerBase
    {
        private readonly IOrderFacad _orderFacad;
        public OrderManagerController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }

        [HttpGet("Id")]
        public IActionResult GET([FromQuery] long OrderId)
        {
            var Result = _orderFacad.GetOrderDetails_AdminService.Execute(OrderId);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                OrderDetail = Result.Data,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "OrderManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UsersOrdersList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "OrderManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "ChangeOrderState",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = "UserId" }, Request.Scheme) ?? "",
                        Rel = "UsersOrdersList",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpGet]
        public IActionResult GET([FromQuery] UsersOrdersListDto _Request)
        {
            var Result = _orderFacad.GetOrders_AdminService.Execute(new RequestGetOrders_AdminDto
            {
                SearchKey = _Request.SearchKey,
                OrderState = _Request.OrderState,
                Page = _Request.Page == 0 ? 1 : _Request.Page,
            }, _Request.PageSize);

            dynamic Output = new
            {
                OrderDetail = Result.Data.OrdersList,
                CurrentPage = Result.Data.CurrentPage,
                PageSize = Result.Data.PageSize,
                RowsCount = Result.Data.RowsCount,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "OrderManager", new { Area = "Admin", OrderId = "OrderId" }, Request.Scheme) ?? "",
                        Rel = "OrderDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "OrderManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "ChangeOrderState",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = "UserId" }, Request.Scheme) ?? "",
                        Rel = "UsersOrdersList",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] ChangeOrderStateDto_Admin _Request)
        {
            var Result = _orderFacad.ChangeOrderState_AdminService.Execute(_Request.OrderId, _Request.OrderState);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "OrderManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UsersOrdersList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "OrderManager", new { Area = "Admin", OrderId = "OrderId" }, Request.Scheme) ?? "",
                        Rel = "OrderDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = "UserId" }, Request.Scheme) ?? "",
                        Rel = "UsersOrdersList",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
