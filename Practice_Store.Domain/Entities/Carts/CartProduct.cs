using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Domain.Entities.Carts
{
    public class CartProduct : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public long ProductSizeId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }

        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }
    }
}
