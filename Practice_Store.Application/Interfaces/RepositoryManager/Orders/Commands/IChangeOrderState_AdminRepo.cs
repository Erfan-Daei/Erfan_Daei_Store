using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands
{
    public interface IChangeOrderState_AdminRepo
    {
        Order? GetOrder(long orderId);
        void Save();
    }
}
