using Practice_Store.Domain.Entities.Orders;

namespace Endpoint.Api.Areas.Admin.Model.OrderManagement
{
    public class ChangeOrderStateDto_Admin
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
    }
}
