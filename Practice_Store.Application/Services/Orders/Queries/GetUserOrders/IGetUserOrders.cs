using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public interface IGetUserOrders
    {
        ResultDto<List<UserOrderDto>> Execute(string UserId);
    }
}
