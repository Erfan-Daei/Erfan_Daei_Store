using Endpoint.Api.Areas.Admin.Model.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.OrderManagement
{
    [Route("api/Area/Admin/OrderManager/[controller]")]
    [Authorize(Policy = "OrderManagementAdmins")]
    [ApiController]
    public class OrderRequestManagerController : ControllerBase
    {
        private readonly IOrderFacad _orderFacad;
        public OrderRequestManagerController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }

        [HttpGet]
        public ActionResult GET([FromQuery] UsersOrderRequestsListDto _Request)
        {
            var Result = (_orderFacad.GetOrderRequest_AdminService.Execute(new RequestGetOrderRequest_AdminDto
            {
                IsPayed = _Request.IsPayed,
                Page = _Request.Page,
                SearchKey = _Request.SearchKey,
            },
            _Request.PageSize));

            dynamic Output = new
            {
                OrderRequestList = Result.Data.OrderRequestList,
                CurrentPage = Result.Data.CurrentPage,
                PageSize = Result.Data.PageSize,
                RowsCount = Result.Data.RowsCount,
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
    }
}
