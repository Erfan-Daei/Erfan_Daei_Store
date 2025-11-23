using Practice_Store.Application.Services.Carts.Commands;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands
{
    public interface IAddToCartRepo
    {
        Cart? GetCart(RequestCartDto Request);

        bool AddCart(Cart cart);

        Product? GetProduct(long productId);

        CartProduct? GetCartProducts(RequestCartDto Request, long CartId);

        bool AddCartProduct(CartProduct cartProduct);

        void Save();
    }
}
