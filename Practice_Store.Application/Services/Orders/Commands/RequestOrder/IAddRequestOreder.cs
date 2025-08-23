using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Commands.RequestOrder
{
    public interface IAddRequestOreder
    {
        ResultDto<ResultAddRequestOrder> Execute(RequestAddRequestOrder Request);
    }
}
