using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands
{
    public interface IAddRequestOrderRepo
    {
        IdtUser? FindUser(string UserId);

        bool AddOrderRequest(OrderRequest orderRequest);

        bool AddExtraInfo(OrderRequestExtraInfo orderRequestExtraInfo);

        void Save();
    }
}
