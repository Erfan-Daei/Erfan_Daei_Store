namespace Endpoint.Api.Model.CartManagement
{
    public class CartManagerDto
    {
        public long ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public int Count { get; set; }
        public Guid BrowserId { get; set; }
    }
}
