using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Commands.AddOrder
{
    public interface IAddOrder
    {
        ResultDto Execute(RequestAddOrder Request);
    }
}
