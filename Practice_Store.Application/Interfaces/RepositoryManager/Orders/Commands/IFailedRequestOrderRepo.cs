using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands
{
    public interface IFailedRequestOrderRepo
    {
        OrderRequest? GetOrderRequest(long OrderRequestId);
        void Save();
    }
}
