using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public class UserOrderDto
    {
        public long OrderId { get; set; }
        public long OrderRequestId { get; set; }
        public long OrderRefId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Mobile { get; set; }

        public int Shipping { get; set; }
        public int TotalPrice { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime? PayDateTime { get; set; }

        public List<UserOrderDetailsDto> UserOrderDetails { get; set; }
    }
}
