using Practice_Store.Domain.Entities.Commons;

namespace Practice_Store.Domain.Entities.Products
{
    public class ProductSizes : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public string Size { get; set; }
        public int Inventory { get; set; }
    }
}
