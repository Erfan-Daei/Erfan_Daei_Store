using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_User
{
    public interface IChangeOrderState_User
    {
        ResultDto Execute(string UserId, long OrderId, OrderState OrderState);
    }
}
