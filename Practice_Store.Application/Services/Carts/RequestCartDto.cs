namespace Practice_Store.Application.Services.Carts
{
    public class RequestCartDto
    {
        public string? UserId { get; set; }
        public long ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public int Count { get; set; }
        public Guid BrowserId { get; set; }
    }
}
