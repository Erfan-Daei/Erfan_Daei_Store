using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetRequestOrder
{
    public class GetRequestOrderService : IGetRequestOrder
    {
        private readonly IDatabaseContext _databaseContext;
        public GetRequestOrderService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultGetRequestOrder> Execute(Guid Guid)
        {
            var _RequestOrder = _databaseContext.OrderRequests.FirstOrDefault(p => p.Guid == Guid);

            return new ResultDto<ResultGetRequestOrder>()
            {
                Data = new ResultGetRequestOrder
                {
                    Id = _RequestOrder.Id,
                    TotalSum = _RequestOrder.TotalSum,
                    Shipping = _RequestOrder.Shipping,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
