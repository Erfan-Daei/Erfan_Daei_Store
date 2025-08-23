using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public class GetUserOrdersService : IGetUserOrders
    {
        private readonly IDatabaseContext _databaseContext;
        public GetUserOrdersService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<UserOrderDto>> Execute(string UserId)
        {
            var _Order = _databaseContext.Orders.Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.OrderRequest)
                .Where(p => p.UserId == UserId.ToString())
                .OrderByDescending(p => p.Id)
                .ToList();

            if (_Order == null)
            {
                return new ResultDto<List<UserOrderDto>>()
                {
                    IsSuccess = false,
                    Message = "هیچ سفارشی یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var OrderList = _Order.Select(o => new UserOrderDto
            {
                Address = o.Address,
                Name = o.Name,
                LastName = o.LastName,
                Mobile = o.Mobile,
                OrderId = o.Id,
                OrderRequestId = o.OrderRequestId,
                OrderState = o.OrderState,
                PostCode = o.PostCode,
                Shipping = o.Shipping,
                TotalPrice = o.Shipping + o.TotalSum,
                PayDateTime = o.OrderRequest.PayDate,
                OrderRefId = o.OrderRequest.RefId,
                UserOrderDetails = o.OrderDetails.Select(d => new UserOrderDetailsDto
                {
                    Count = d.Count,
                    OrderDetailId = d.Id,
                    Price = d.ProductPrice,
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    ProductSizeName = d.ProductSizeName,
                    ProductImageSrc = d.Product.ProductImages.FirstOrDefault(i => i.ProductId == d.ProductId).Src
                }).ToList(),
            }).ToList();

            return new ResultDto<List<UserOrderDto>>()
            {
                Data = OrderList,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
