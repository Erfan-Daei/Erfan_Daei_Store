using Practice_Store.Domain.Entities.Orders;

namespace Endpoint.Api.Model.OrderManagement
{
    public class ChangeOrderStateDto_User
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
    }
}
