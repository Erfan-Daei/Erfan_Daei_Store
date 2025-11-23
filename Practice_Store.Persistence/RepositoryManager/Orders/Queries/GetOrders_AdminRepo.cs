using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Queries
{
    public class GetOrders_AdminRepo : IGetOrders_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrders_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<Order>? GetOrders()
        {
            return _databaseContext.Orders
                .Include(p => p.OrderRequest)
                .AsQueryable();
        }
    }
}
