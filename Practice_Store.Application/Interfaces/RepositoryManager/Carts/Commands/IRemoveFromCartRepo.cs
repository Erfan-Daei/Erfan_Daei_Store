using Practice_Store.Application.Services.Carts.Commands;
using Practice_Store.Domain.Entities.Carts;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands
{
    public interface IRemoveFromCartRepo
    {
        CartProduct? GetCartProduct(RequestCartDto Request);

        void Save();
    }
}
