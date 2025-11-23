using Practice_Store.Common;

namespace Practice_Store.Application.Services.Carts.Commands.RemoveFromCart
{
    public interface IRemoveFromCart
    {
        ResultDto RemoveFromCart(RequestCartDto Request);
    }
}
