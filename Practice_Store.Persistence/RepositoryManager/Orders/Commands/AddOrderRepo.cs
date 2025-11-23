using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Products;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Commands
{
    public class AddOrderRepo : IAddOrderRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public AddOrderRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IdtUser? FindUser(string UserId)
        {
            return _databaseContext.Users.Find(UserId);
        }

        public OrderRequest? GetOrderRequest(long OrderRequestId)
        {
            return _databaseContext.OrderRequests
                .Where(p => p.Id == OrderRequestId)
                .Include(p => p.OrderRequestExtraInfo)
                .FirstOrDefault();
        }

        public Cart? GetCart(long CartId)
        {
            return _databaseContext.Carts
                .Where(p => p.Id == CartId)
                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductSizes)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Off)
                .FirstOrDefault();
        }

        public bool AddOrder(Order order)
        {
            try
            {
                _databaseContext.Orders.Add(order);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ProductSizes? GetProductSize(long ProductSizeId)
        {
            return _databaseContext.ProductSizes.Find(ProductSizeId);
        }

        public bool AddOrderDetail(List<OrderDetail> orderDetails)
        {
            try
            {
                _databaseContext.OrderDetails.AddRange(orderDetails);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
