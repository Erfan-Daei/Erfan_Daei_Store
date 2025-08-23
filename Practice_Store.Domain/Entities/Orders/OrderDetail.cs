using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Domain.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }

        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public long ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }

        public int ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
