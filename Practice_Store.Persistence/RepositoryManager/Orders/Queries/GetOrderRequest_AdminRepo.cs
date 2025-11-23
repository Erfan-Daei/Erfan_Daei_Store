using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Queries
{
    public class GetOrderRequest_AdminRepo : IGetOrderRequest_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrderRequest_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<OrderRequest>? GetOrderRequest()
        {
            return _databaseContext.OrderRequests
                .Include(p => p.OrderRequestExtraInfo)
                .AsQueryable();
        }
    }
}
