using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
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

namespace Practice_Store.Application.ServiceCollection
{
    public class OrderFacad : IOrderFacad
    {
        private readonly IDatabaseContext _databaseContext;
        public OrderFacad(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        private IAddRequestOreder _addRequestOreder;
        public IAddRequestOreder AddRequestOrederService
        {
            get
            {
                return _addRequestOreder = _addRequestOreder ?? new AddRequestOrderService(_databaseContext);
            }
        }

        private IGetRequestOrder _getRequestOrder;
        public IGetRequestOrder GetRequestOrderService
        {
            get
            {
                return _getRequestOrder = _getRequestOrder ?? new GetRequestOrderService(_databaseContext);
            }
        }

        private IAddOrder _addOrder;
        public IAddOrder AddOrderService
        {
            get
            {
                return _addOrder = _addOrder ?? new AddOrderService(_databaseContext);
            }
        }

        private IGetUserOrders _getUserOrders;
        public IGetUserOrders GetUserOrdersService
        {
            get
            {
                return _getUserOrders = _getUserOrders ?? new GetUserOrdersService(_databaseContext);
            }
        }

        private IChangeOrderState_User _changeOrderState_User;
        public IChangeOrderState_User ChangeOrderState_UserService
        {
            get
            {
                return _changeOrderState_User = _changeOrderState_User ?? new ChangeOrderState_UserService(_databaseContext);
            }
        }

        private IUpdateFailedRequestOrder _updateFailedRequestOrder;
        public IUpdateFailedRequestOrder UpdateFailedRequestOrderService
        {
            get
            {
                return _updateFailedRequestOrder = _updateFailedRequestOrder ?? new UpdateFailedRequestOrderService(_databaseContext);
            }
        }

        private IGetOrders_Admin _getOrders_Admin;
        public IGetOrders_Admin GetOrders_AdminService
        {
            get
            {
                return _getOrders_Admin = _getOrders_Admin ?? new GetOrders_AdminService(_databaseContext);
            }
        }

        private IGetOrderDetails_Admin _getOrderDetails_Admin;
        public IGetOrderDetails_Admin GetOrderDetails_AdminService
        {
            get
            {
                return _getOrderDetails_Admin = _getOrderDetails_Admin ?? new GetOrderDetails_AdminService(_databaseContext);
            }
        }

        private IChangeOrderState_Admin _changeOrderState_Admin;
        public IChangeOrderState_Admin ChangeOrderState_AdminService
        {
            get
            {
                return _changeOrderState_Admin = _changeOrderState_Admin ?? new ChangeOrderState_AdminService(_databaseContext);
            }
        }

        private IGetOrderRequest_Admin _getOrderRequest_Admin;
        public IGetOrderRequest_Admin GetOrderRequest_AdminService
        {
            get
            {
                return _getOrderRequest_Admin = _getOrderRequest_Admin ?? new GetOrderRequest_AdminService(_databaseContext);
            }
        }
    }
}
