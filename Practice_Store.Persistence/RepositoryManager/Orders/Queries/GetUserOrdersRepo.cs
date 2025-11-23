using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Queries
{
    public class GetUserOrdersRepo : IGetUserOrdersRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetUserOrdersRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Order> GetOrders(string UserId)
        {
            return _databaseContext.Orders
                .Where(p => p.UserId == UserId.ToString())
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(p => p.OrderRequest)
                .OrderByDescending(p => p.Id)
                .ToList();
        }
    }
}
