using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries
{
    public interface IGetRequestOrderRepo
    {
        OrderRequest? GetOrderRequest(Guid guid);

    }
}
