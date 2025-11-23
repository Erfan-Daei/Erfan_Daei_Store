using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public class GetUserOrdersService : IGetUserOrders
    {
        private readonly IGetUserOrdersRepo _getUserOrdersRepo;
        public GetUserOrdersService(IGetUserOrdersRepo getUserOrdersRepo)
        {
            _getUserOrdersRepo = getUserOrdersRepo;
        }

        public ResultDto<List<UserOrderDto>> Execute(string UserId)
        {
            var _Order = _getUserOrdersRepo.GetOrders(UserId);

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
