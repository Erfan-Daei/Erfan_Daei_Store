using Practice_Store.Application.Services.Carts.Commands.AddToCart;
using Practice_Store.Application.Services.Carts.Commands.RemoveFromCart;
using Practice_Store.Application.Services.Carts.Queries.GetCart;

namespace Practice_Store.Application.Interfaces.FacadPatterns
{
    public interface ICartFacad
    {
        IAddToCart AddToCartService { get; }
        IRemoveFromCart RemoveFromCartService { get; }
        IGetCart GetCartService { get; }
    }
}
