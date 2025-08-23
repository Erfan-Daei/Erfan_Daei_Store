using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Domain.Entities.Orders
{
    public class OrderRequest : BaseEntity
    {
        public Guid Guid { get; set; }
        public virtual IdtUser User { get; set; }
        public string UserId { get; set; }
        public int TotalSum { get; set; }
        public int Shipping { get; set; }
        public bool IsPayed { get; set; } = false;
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; } = "Not Authorized";
        public long RefId { get; set; } = 0;

        public OrderRequestExtraInfo OrderRequestExtraInfo { get; set; }
    }
}
