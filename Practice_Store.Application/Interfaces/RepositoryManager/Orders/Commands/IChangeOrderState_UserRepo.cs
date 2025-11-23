using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands
{
    public interface IChangeOrderState_UserRepo
    {
        Order? GetOrder(long OrderId, string UserId);
        void Save();
    }
}
