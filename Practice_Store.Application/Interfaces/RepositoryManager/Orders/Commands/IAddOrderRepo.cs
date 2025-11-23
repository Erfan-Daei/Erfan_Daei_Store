using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Products;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands
{
    public interface IAddOrderRepo
    {
        IdtUser? FindUser(string UserId);

        OrderRequest? GetOrderRequest(long OrderRequestId);

        Cart? GetCart(long CartId);

        bool AddOrder(Order order);

        ProductSizes? GetProductSize(long ProductSizeId);

        bool AddOrderDetail(List<OrderDetail> orderDetails);

        void Save();
    }
}
