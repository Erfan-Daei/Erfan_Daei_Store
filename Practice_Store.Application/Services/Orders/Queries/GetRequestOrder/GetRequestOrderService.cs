using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Application.Interfaces.ZarinPal;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetRequestOrder
{
    public class GetRequestOrderService : IGetRequestOrder
    {
        private readonly IGetRequestOrderRepo _getRequestOrderRepo;
        private readonly IManageZarinPal _manageZarinPal;
        public GetRequestOrderService(IGetRequestOrderRepo getRequestOrderRepo,
            IManageZarinPal manageZarinPal)
        {
            _getRequestOrderRepo = getRequestOrderRepo;
            _manageZarinPal = manageZarinPal;
        }

        public ResultDto<ResultGetRequestOrder> Execute(Guid Guid, string Authority)
        {
            var _RequestOrder = _getRequestOrderRepo.GetOrderRequest(Guid);

            var ZarinPal = _manageZarinPal.ValidateRequestFromZarinPal(new ResultValidateRequestFromZarinPalDto
            {
                Amount = _RequestOrder.TotalSum + _RequestOrder.Shipping,
                Authority = Authority,
            });

            return new ResultDto<ResultGetRequestOrder>()
            {
                Data = new ResultGetRequestOrder
                {
                    Id = _RequestOrder.Id,
                    RefId = ZarinPal.Result.RefId,
                    Code = ZarinPal.Result.Code,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
