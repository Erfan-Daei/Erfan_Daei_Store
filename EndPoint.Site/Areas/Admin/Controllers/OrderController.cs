using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin;
using Practice_Store.Domain.Entities.Orders;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,OrderManagement_Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderFacad _orderFacad;
        public OrderController(IOrderFacad orderFacad)
        {
            _orderFacad = orderFacad;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUserOrders(RequestGetOrders_AdminDto Request, int PageSize = 20)
        {
            return View(_orderFacad.GetOrders_AdminService.Execute(new RequestGetOrders_AdminDto
            {
                SearchKey = Request.SearchKey,
                OrderState = Request.OrderState,
                Page = Request.Page == 0 ? 1 : Request.Page,
            }, PageSize).Data);
        }

        [HttpGet]
        public IActionResult GetOrderDetails(long OrderId)
        {
            return View(_orderFacad.GetOrderDetails_AdminService.Execute(OrderId).Data);
        }

        [HttpPatch]
        public IActionResult ChangeOrderState(long OrderId, OrderState OrderState)
        {
            return Json(_orderFacad.ChangeOrderState_AdminService.Execute(OrderId, OrderState));
        }

        [HttpGet]
        public IActionResult GetOrderRequests(RequestGetOrderRequest_AdminDto Request, int PageSize = 20)
        {
            return View(_orderFacad.GetOrderRequest_AdminService.Execute(new RequestGetOrderRequest_AdminDto
            {
                SearchKey = Request.SearchKey,
                IsPayed = Request.IsPayed,
                Page = Request.Page == 0 ? 1 : Request.Page,
            }, PageSize).Data);
        }
    }
}
