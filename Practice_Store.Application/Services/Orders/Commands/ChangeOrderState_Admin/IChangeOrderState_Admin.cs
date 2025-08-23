using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_Admin
{
    public interface IChangeOrderState_Admin
    {
        ResultDto Execute(long OrderId, OrderState OrderState);
    }
}