using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.AddOrder
{
    public class AddOrderService : IAddOrder
    {
        private readonly IDatabaseContext _databaseContext;
        public AddOrderService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(RequestAddOrder Request)
        {
            var _User = _databaseContext.Users.Find(Request.UserId);
            var _OrderRequest = _databaseContext.OrderRequests.Include(p => p.OrderRequestExtraInfo).FirstOrDefault(p => p.Id == Request.OrderRequestId);

            var _Cart = _databaseContext.Carts.Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductSizes)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Off)
                .FirstOrDefault(p => p.Id == Request.CartId);

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
            _databaseContext.Orders.Add(Order);

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
                var Size = _databaseContext.ProductSizes.Find(item.ProductSizeId);
                Size.Inventory = Size.Inventory - item.Count;
                orderDetail.ProductSizeName = Size.Size;
            }
            _databaseContext.OrderDetails.AddRange(OrderDetails);
            Order.OrderDetails = OrderDetails;

            _databaseContext.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
