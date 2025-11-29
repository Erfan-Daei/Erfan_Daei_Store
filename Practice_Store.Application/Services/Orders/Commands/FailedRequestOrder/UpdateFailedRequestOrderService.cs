using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Commands.FailedRequestOrder
{
    public class UpdateFailedRequestOrderService : IUpdateFailedRequestOrder
    {
        private readonly IFailedRequestOrderRepo _failedRequestOrderRepo;
        public UpdateFailedRequestOrderService(IFailedRequestOrderRepo failedRequestOrderRepo)
        {
            _failedRequestOrderRepo = failedRequestOrderRepo;
        }

        public ResultDto Execute(long OrderRequestId, string Authority, long RefId)
        {
            var _OrderRequest = _failedRequestOrderRepo.GetOrderRequest(OrderRequestId);

            _OrderRequest.Authority = Authority;
            _OrderRequest.RefId = RefId;
            _failedRequestOrderRepo.Save();
            return new ResultDto
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
