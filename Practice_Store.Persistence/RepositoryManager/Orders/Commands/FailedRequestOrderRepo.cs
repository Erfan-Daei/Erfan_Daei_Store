using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Commands
{
    public class FailedRequestOrderRepo : IFailedRequestOrderRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public FailedRequestOrderRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public OrderRequest? GetOrderRequest(long OrderRequestId)
        {
            return _databaseContext.OrderRequests.Find(OrderRequestId);
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
