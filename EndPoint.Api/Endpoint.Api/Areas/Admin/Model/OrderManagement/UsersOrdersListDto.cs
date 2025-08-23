using Practice_Store.Domain.Entities.Orders;

namespace Endpoint.Api.Areas.Admin.Model.OrderManagement
{
    public class UsersOrdersListDto
    {
        public OrderState? OrderState { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
