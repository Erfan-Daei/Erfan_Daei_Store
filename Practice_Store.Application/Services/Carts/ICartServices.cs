using Practice_Store.Common;

namespace Practice_Store.Application.Services.Carts
{
    public interface ICartServices
    {
        ResultDto AddToCart(RequestCartDto Request);
        ResultDto RemoveFromCart(RequestCartDto Request);
        ResultDto<CartDto> GetCart(Guid BrowserId, string? UserId);
    }
}
