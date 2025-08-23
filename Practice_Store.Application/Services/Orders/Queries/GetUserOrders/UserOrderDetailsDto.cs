namespace Practice_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public class UserOrderDetailsDto
    {
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageSrc { get; set; }
        public string ProductSizeName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
