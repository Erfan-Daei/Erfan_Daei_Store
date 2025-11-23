using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Quesries;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Carts.Queries
{
    public class GetCartRepo : IGetCartRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetCartRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Cart? GetCart(Guid BrowserId, string? UserId)
        {
            return _databaseContext.Carts
                .Where(p => p.BrowserId == BrowserId && !p.IsDone && (p.UserId == null || p.UserId == UserId))

                .Include(c => c.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductSizes)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Off)

                .OrderByDescending(p => p.Id)
                .FirstOrDefault();
        }

        public bool AddCart(Cart cart)
        {
            try
            {
                _databaseContext.Carts.Add(cart);
                _databaseContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IdtUser? FindUser(string UserId)
        {
            return _databaseContext.Users.Find(UserId);
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
