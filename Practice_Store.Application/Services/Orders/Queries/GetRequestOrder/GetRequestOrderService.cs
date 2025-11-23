using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetRequestOrder
{
    public class GetRequestOrderService : IGetRequestOrder
    {
        private readonly IGetRequestOrderRepo _getRequestOrderRepo;
        public GetRequestOrderService(IGetRequestOrderRepo getRequestOrderRepo)
        {
            _getRequestOrderRepo = getRequestOrderRepo;
        }

        public ResultDto<ResultGetRequestOrder> Execute(Guid Guid)
        {
            var _RequestOrder = _getRequestOrderRepo.GetOrderRequest(Guid);

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
