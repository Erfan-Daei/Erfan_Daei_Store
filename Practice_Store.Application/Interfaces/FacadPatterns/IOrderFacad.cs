using Practice_Store.Application.Services.Orders.Commands.AddOrder;
using Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_Admin;
using Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_User;
using Practice_Store.Application.Services.Orders.Commands.FailedRequestOrder;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;
using Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetRequestOrder;
using Practice_Store.Application.Services.Orders.Queries.GetUserOrders;

namespace Practice_Store.Application.Interfaces.FacadPatterns
{
    public interface IOrderFacad
    {
        IAddRequestOreder AddRequestOrederService { get; }
        IGetRequestOrder GetRequestOrderService { get; }
        IAddOrder AddOrderService { get; }
        IGetUserOrders GetUserOrdersService { get; }
        IChangeOrderState_User ChangeOrderState_UserService { get; }
        IUpdateFailedRequestOrder UpdateFailedRequestOrderService { get; }
        IGetOrders_Admin GetOrders_AdminService { get; }
        IGetOrderDetails_Admin GetOrderDetails_AdminService { get; }
        IChangeOrderState_Admin ChangeOrderState_AdminService { get; }
        IGetOrderRequest_Admin GetOrderRequest_AdminService { get; }
    }
}
