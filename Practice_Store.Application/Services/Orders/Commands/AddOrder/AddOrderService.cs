using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.AddOrder
{
    public class AddOrderService : IAddOrder
    {
        private readonly IAddOrderRepo _addOrderRepo;
        public AddOrderService(IAddOrderRepo addOrderRepo)
        {
            _addOrderRepo = addOrderRepo;
        }

        public ResultDto Execute(RequestAddOrder Request)
        {
            var _User = _addOrderRepo.FindUser(Request.UserId);
            var _OrderRequest = _addOrderRepo.GetOrderRequest(Request.OrderRequestId);

            var _Cart = _addOrderRepo.GetCart(Request.CartId);

            _OrderRequest.IsPayed = true;
            _OrderRequest.PayDate = DateTime.Now;
            _OrderRequest.Authority = Request.Authority;
            _OrderRequest.RefId = Request.RefId;

            _Cart.IsDone = true;

            Order Order = new Order()
            {
                Address = _OrderRequest.OrderRequestExtraInfo.Address,
                Name = _OrderRequest.OrderRequestExtraInfo.Name,
                LastName = _OrderRequest.OrderRequestExtraInfo.LastName,
                Mobile = _OrderRequest.OrderRequestExtraInfo.Mobile.ToString(),
                PostCode = _OrderRequest.OrderRequestExtraInfo.PostCode,
                OrderRequest = _OrderRequest,
                User = _User,
                OrderState = OrderState.Processing,
                Shipping = _OrderRequest.Shipping,
                TotalSum = _OrderRequest.TotalSum,
            };
            var Add = _addOrderRepo.AddOrder(Order);

            List<OrderDetail> OrderDetails = new List<OrderDetail>();
            foreach (var item in _Cart.CartProducts)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Product = item.Product,
                    Count = item.Count,
                    Order = Order,
                    ProductPrice = item.Product.Price - ((item.Product.Price * item.Product.Off?.Percentage ?? 0) / 100),
                    ProductSizeId = item.ProductSizeId,
                };
                OrderDetails.Add(orderDetail);
                var Size = _addOrderRepo.GetProductSize(item.ProductSizeId);
                Size.Inventory = Size.Inventory - item.Count;
                orderDetail.ProductSizeName = Size.Size;
            }
            var AddDetails = _addOrderRepo.AddOrderDetail(OrderDetails);
            Order.OrderDetails = OrderDetails;

            _addOrderRepo.Save();
            return new ResultDto
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
