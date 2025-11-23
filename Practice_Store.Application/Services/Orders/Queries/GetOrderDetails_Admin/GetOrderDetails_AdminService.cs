using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin
{
    public class GetOrderDetails_AdminService : IGetOrderDetails_Admin
    {
        private readonly IGetOrderDetails_AdminRepo _getOrderDetails_AdminRepo;
        public GetOrderDetails_AdminService(IGetOrderDetails_AdminRepo getOrderDetails_AdminRepo)
        {
            _getOrderDetails_AdminRepo = getOrderDetails_AdminRepo;
        }

        public ResultDto<GetOrderDetails_AdminDto> Execute(long OrderId)
        {
            var _Order = _getOrderDetails_AdminRepo.GetOrder(OrderId);

            return new ResultDto<GetOrderDetails_AdminDto>
            {
                Data = new GetOrderDetails_AdminDto
                {
                    OrderId = OrderId,
                    UserId = _Order.UserId,
                    OrderRequestId = _Order.OrderRequestId,
                    RefId = _Order.OrderRequest.RefId,

                    Address = _Order.Address,
                    PostCode = _Order.PostCode,
                    Name = _Order.Name,
                    LastName = _Order.LastName,
                    Mobile = _Order.Mobile,
                    Shipping = _Order.Shipping,
                    TotalSum = _Order.TotalSum,
                    PayDateTime = _Order.OrderRequest.PayDate,
                    OrderState = _Order.OrderState,
                    OrderDetails = _Order.OrderDetails.Select(p => new OrderDetails_AdminDto
                    {
                        ProductId = p.ProductId,
                        Count = p.Count,
                        ProductPrice = p.ProductPrice,
                        ProductName = p.Product.Name,
                        ProductImageSrc = p.Product.ProductImages.FirstOrDefault(i => i.ProductId == p.ProductId).Src,
                        ProductSizeName = p.ProductSizeName
                    }).ToList(),
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
