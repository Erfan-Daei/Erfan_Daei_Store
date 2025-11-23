using Practice_Store.Common;

namespace Practice_Store.Application.Services.Carts.Commands.AddToCart
{
    public interface IAddToCart
    {
        ResultDto AddToCart(RequestCartDto Request);
    }
}
