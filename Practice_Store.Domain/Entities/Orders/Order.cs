using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public virtual IdtUser User { get; set; }
        public string UserId { get; set; }

        public virtual OrderRequest OrderRequest { get; set; }
        public long OrderRequestId { get; set; }

        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string? Mobile { get; set; }
        public int Shipping { get; set; }
        public int TotalSum { get; set; }

        public OrderState OrderState { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
