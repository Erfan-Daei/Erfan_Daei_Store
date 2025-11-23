using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Commands
{
    public class ChangeOrderState_UserRepo : IChangeOrderState_UserRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public ChangeOrderState_UserRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Order? GetOrder(long OrderId, string UserId)
        {
            return _databaseContext.Orders
                .FirstOrDefault(p => p.Id == OrderId &&
                p.UserId == UserId.ToString() &&
                (p.OrderState != OrderState.AdminCanceled && p.OrderState != OrderState.UserCanceled && p.OrderState != OrderState.Delivered));
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
