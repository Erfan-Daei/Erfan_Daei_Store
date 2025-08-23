using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin
{
    public class GetOrderDetails_AdminDto
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public long OrderRequestId { get; set; }
        public long RefId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Mobile { get; set; }
        public int Shipping { get; set; }
        public int TotalSum { get; set; }
        public DateTime? PayDateTime { get; set; }

        public OrderState OrderState { get; set; }

        public List<OrderDetails_AdminDto> OrderDetails { get; set; }
    }
}
