namespace Practice_Store.Application.Services.Carts
{
    public class CartProductDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public int ProductSizeInventory {  get; set; }
        public string ProductImageSrc { get; set; }
        public int ProductOff {  get; set; }
        public int ProductPrice { get; set; }
        public int Count { get; set; }
        public int ProductTotalSum { get; set; }
        public long CartId { get; set; }
    }
}
