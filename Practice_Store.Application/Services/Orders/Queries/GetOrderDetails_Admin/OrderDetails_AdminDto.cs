namespace Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin
{
    public class OrderDetails_AdminDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSizeName { get; set; }
        public string ProductImageSrc { get; set; }

        public int ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
