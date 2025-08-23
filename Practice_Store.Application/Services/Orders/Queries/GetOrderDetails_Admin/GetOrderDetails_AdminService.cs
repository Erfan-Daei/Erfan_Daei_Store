using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin
{
    public class GetOrderDetails_AdminService : IGetOrderDetails_Admin
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrderDetails_AdminService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<GetOrderDetails_AdminDto> Execute(long OrderId)
        {
            var _Order = _databaseContext.Orders.Include(p => p.OrderRequest)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == OrderId);

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
