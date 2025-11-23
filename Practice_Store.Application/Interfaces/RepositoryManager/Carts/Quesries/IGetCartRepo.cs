using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Carts.Quesries
{
    public interface IGetCartRepo
    {
        Cart? GetCart(Guid BrowserId, string? UserId);

        bool AddCart(Cart cart);

        IdtUser? FindUser(string UserId);

        void Save();
    }
}
