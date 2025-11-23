using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Queries
{
    public class GetOrderDetails_AdminRepo : IGetOrderDetails_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrderDetails_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Order? GetOrder(long OrderId)
        {
            return _databaseContext.Orders
                .Where(p => p.Id == OrderId)
                .Include(p => p.OrderRequest)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefault();
        }
    }
}
