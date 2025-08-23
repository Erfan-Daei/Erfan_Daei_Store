using Practice_Store.Domain.Entities.Commons;

namespace Practice_Store.Domain.Entities.Products
{
    public class ProductOff : BaseEntity
    {
        public byte Percentage { get; set; }

        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
