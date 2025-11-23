using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries
{
    public interface IGetOrders_AdminRepo
    {
        IQueryable<Order>? GetOrders();
    }
}
