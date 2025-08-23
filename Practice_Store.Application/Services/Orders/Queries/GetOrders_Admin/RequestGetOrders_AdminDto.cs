using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin
{
    public class RequestGetOrders_AdminDto
    {
        public OrderState? OrderState { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; }
    }
}
