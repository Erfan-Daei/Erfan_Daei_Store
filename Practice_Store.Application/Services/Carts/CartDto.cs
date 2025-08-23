namespace Practice_Store.Application.Services.Carts
{
    public class CartDto
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public List<CartProductDto>? CartProducts { get; set; }
        public int TotalSum { get; set; }
    }
}
