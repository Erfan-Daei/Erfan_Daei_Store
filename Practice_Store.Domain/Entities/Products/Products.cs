using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public bool Displayed { get; set; }
        public int ViewCount { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }

        public ICollection<ProductImages>? ProductImages { get; set; }
        public ICollection<ProductSizes>? ProductSizes { get; set; }


        public ProductOff? Off { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

        public ICollection<Review>? Reviews { get; set; }
        public int ReviewCount { get; set; } = 0;
        public float ReviewScore { get; set; } = 0;
    }
}
