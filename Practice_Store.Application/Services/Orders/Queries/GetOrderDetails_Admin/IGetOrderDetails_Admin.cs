using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin
{
    public interface IGetOrderDetails_Admin
    {
        ResultDto<GetOrderDetails_AdminDto> Execute(long OrderId);
    }
}
