using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Commands.FailedRequestOrder
{
    public interface IUpdateFailedRequestOrder
    {
        ResultDto Execute(long OrderRequestId, string Authority, long RefId);
    }
}
