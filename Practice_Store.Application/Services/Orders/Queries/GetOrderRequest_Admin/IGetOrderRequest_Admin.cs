using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin
{
    public interface IGetOrderRequest_Admin
    {
        ResultDto<ResultGetOrderRequest_AdminDto> Execute(RequestGetOrderRequest_AdminDto Request, int PageSize = 20);
    }
}
