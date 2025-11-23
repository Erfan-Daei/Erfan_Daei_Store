using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Queries
{
    public class GetRequestOrderRepo : IGetRequestOrderRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetRequestOrderRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public OrderRequest? GetOrderRequest(Guid guid)
        {
            return _databaseContext.OrderRequests.FirstOrDefault(p => p.Guid == guid);
        }
    }
}
