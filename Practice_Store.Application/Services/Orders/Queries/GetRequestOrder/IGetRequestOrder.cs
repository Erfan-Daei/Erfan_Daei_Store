using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetRequestOrder
{
    public interface IGetRequestOrder
    {
        ResultDto<ResultGetRequestOrder> Execute(Guid Guid);
    }
}
