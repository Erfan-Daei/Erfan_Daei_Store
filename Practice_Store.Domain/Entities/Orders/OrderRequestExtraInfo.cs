using Practice_Store.Domain.Entities.Commons;

namespace Practice_Store.Domain.Entities.Orders
{
    public class OrderRequestExtraInfo : BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Mobile { get; set; }

        public virtual OrderRequest OrderRequest { get; set; }
        public long OrderRequestId { get; set; }
    }
}
