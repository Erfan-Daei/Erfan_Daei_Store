using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin
{
    public interface IGetOrders_Admin
    {
        ResultDto<ResultGetOrders_AdminDto> Execute(RequestGetOrders_AdminDto Request, int PageSize = 20);
    }
}
