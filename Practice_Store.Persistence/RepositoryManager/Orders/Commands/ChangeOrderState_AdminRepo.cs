using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Commands
{
    public class ChangeOrderState_AdminRepo : IChangeOrderState_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public ChangeOrderState_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Order? GetOrder(long orderId)
        {
            return _databaseContext.Orders
                .FirstOrDefault(p => p.Id == orderId &&
                (p.OrderState != OrderState.UserCanceled && p.OrderState != OrderState.Delivered));
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
