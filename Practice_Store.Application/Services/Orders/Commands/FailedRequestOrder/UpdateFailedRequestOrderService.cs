using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Commands.FailedRequestOrder
{
    public class UpdateFailedRequestOrderService : IUpdateFailedRequestOrder
    {
        private readonly IDatabaseContext _databaseContext;
        public UpdateFailedRequestOrderService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(long OrderRequestId, string Authority, long RefId)
        {
            var _OrderRequest = _databaseContext.OrderRequests.Find(OrderRequestId);

            _OrderRequest.Authority = Authority;
            _OrderRequest.RefId = RefId;
            _databaseContext.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Status_Code = Status_Code.OK,
            };
        }
    }
}
