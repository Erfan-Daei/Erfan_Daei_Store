using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
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
        private readonly IAddOrderRepo _addOrderRepo;
        private readonly IChangeOrderState_AdminRepo _changeOrderState_AdminRepo;
        private readonly IChangeOrderState_UserRepo _changeOrderState_UserRepo;
        private readonly IFailedRequestOrderRepo _failedRequestOrderRepo;
        private readonly IAddRequestOrderRepo _addRequestOrderRepo;
        private readonly IGetOrderDetails_AdminRepo _getOrderDetails_AdminRepo;
        private readonly IGetOrderRequest_AdminRepo _getOrderRequest_AdminRepo;
        private readonly IGetOrders_AdminRepo _getOrders_AdminRepo;
        private readonly IGetRequestOrderRepo _getRequestOrderRepo;
        private readonly IGetUserOrdersRepo _getUserOrdersRepo;
        public OrderFacad(IAddOrderRepo addOrderRepo,
            IChangeOrderState_AdminRepo changeOrderState_AdminRepo,
            IChangeOrderState_UserRepo changeOrderState_UserRepo,
            IFailedRequestOrderRepo failedRequestOrderRepo,
            IAddRequestOrderRepo requestOrderRepo,
            IGetOrderDetails_AdminRepo getOrderDetails_AdminRepo,
            IGetOrderRequest_AdminRepo getOrderRequest_AdminRepo,
            IGetOrders_AdminRepo getOrders_AdminRepo,
            IGetRequestOrderRepo getRequestOrderRepo,
            IGetUserOrdersRepo getUserOrdersRepo)
        {
            _addOrderRepo = addOrderRepo;
            _changeOrderState_AdminRepo = changeOrderState_AdminRepo;
            _changeOrderState_UserRepo = changeOrderState_UserRepo;
            _addRequestOrderRepo = requestOrderRepo;
            _failedRequestOrderRepo = failedRequestOrderRepo;
            _getOrderDetails_AdminRepo = getOrderDetails_AdminRepo;
            _getOrderRequest_AdminRepo = getOrderRequest_AdminRepo;
            _getUserOrdersRepo = getUserOrdersRepo;
            _getOrders_AdminRepo = getOrders_AdminRepo;
            _getRequestOrderRepo = getRequestOrderRepo;
        }

        private IAddRequestOreder _addRequestOreder;
        public IAddRequestOreder AddRequestOrederService
        {
            get
            {
                return _addRequestOreder = _addRequestOreder ?? new AddRequestOrderService(_addRequestOrderRepo);
            }
        }

        private IGetRequestOrder _getRequestOrder;
        public IGetRequestOrder GetRequestOrderService
        {
            get
            {
                return _getRequestOrder = _getRequestOrder ?? new GetRequestOrderService(_getRequestOrderRepo);
            }
        }

        private IAddOrder _addOrder;
        public IAddOrder AddOrderService
        {
            get
            {
                return _addOrder = _addOrder ?? new AddOrderService(_addOrderRepo);
            }
        }

        private IGetUserOrders _getUserOrders;
        public IGetUserOrders GetUserOrdersService
        {
            get
            {
                return _getUserOrders = _getUserOrders ?? new GetUserOrdersService(_getUserOrdersRepo);
            }
        }

        private IChangeOrderState_User _changeOrderState_User;
        public IChangeOrderState_User ChangeOrderState_UserService
        {
            get
            {
                return _changeOrderState_User = _changeOrderState_User ?? new ChangeOrderState_UserService(_changeOrderState_UserRepo);
            }
        }

        private IUpdateFailedRequestOrder _updateFailedRequestOrder;
        public IUpdateFailedRequestOrder UpdateFailedRequestOrderService
        {
            get
            {
                return _updateFailedRequestOrder = _updateFailedRequestOrder ?? new UpdateFailedRequestOrderService(_failedRequestOrderRepo);
            }
        }

        private IGetOrders_Admin _getOrders_Admin;
        public IGetOrders_Admin GetOrders_AdminService
        {
            get
            {
                return _getOrders_Admin = _getOrders_Admin ?? new GetOrders_AdminService(_getOrders_AdminRepo);
            }
        }

        private IGetOrderDetails_Admin _getOrderDetails_Admin;
        public IGetOrderDetails_Admin GetOrderDetails_AdminService
        {
            get
            {
                return _getOrderDetails_Admin = _getOrderDetails_Admin ?? new GetOrderDetails_AdminService(_getOrderDetails_AdminRepo);
            }
        }

        private IChangeOrderState_Admin _changeOrderState_Admin;
        public IChangeOrderState_Admin ChangeOrderState_AdminService
        {
            get
            {
                return _changeOrderState_Admin = _changeOrderState_Admin ?? new ChangeOrderState_AdminService(_changeOrderState_AdminRepo);
            }
        }

        private IGetOrderRequest_Admin _getOrderRequest_Admin;
        public IGetOrderRequest_Admin GetOrderRequest_AdminService
        {
            get
            {
                return _getOrderRequest_Admin = _getOrderRequest_Admin ?? new GetOrderRequest_AdminService(_getOrderRequest_AdminRepo);
            }
        }
    }
}
