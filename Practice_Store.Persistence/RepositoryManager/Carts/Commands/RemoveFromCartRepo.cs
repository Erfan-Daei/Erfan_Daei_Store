using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Application.Services.Carts.Commands;
using Practice_Store.Domain.Entities.Carts;

namespace Practice_Store.Persistence.RepositoryManager.Carts.Commands
{
    public class RemoveFromCartRepo : IRemoveFromCartRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public RemoveFromCartRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public CartProduct? GetCartProduct(RequestCartDto Request)
        {
            return _databaseContext.CartProducts
                .Where(p => p.Cart.BrowserId == Request.BrowserId &&
                (p.Cart.UserId == null || p.Cart.UserId == Request.UserId.ToString()) &&
                p.ProductId == Request.ProductId && p.ProductSizeId == Request.ProductSizeId)
                .FirstOrDefault();
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
