using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Application.Services.Carts.Commands;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Carts.Commands
{
    public class AddToCartRepo : IAddToCartRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public AddToCartRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Cart? GetCart(RequestCartDto Request)
        {
            return _databaseContext.Carts.Where(p => p.BrowserId == Request.BrowserId && !p.IsDone && (p.UserId == null || p.UserId == Request.UserId.ToString()))
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

        public Product? GetProduct(long productId)
        {
            return _databaseContext.Products
                .Where(p => p.Id == productId)
                .Include(p => p.ProductSizes)
                .Include(p => p.Off)
                .FirstOrDefault();
        }

        public CartProduct? GetCartProducts(RequestCartDto Request, long CartId)
        {
            return _databaseContext.CartProducts
                .Where(p => p.ProductId == Request.ProductId && p.ProductSizeId == Request.ProductSizeId && p.CartId == CartId)
                .FirstOrDefault();
        }

        public bool AddCartProduct(CartProduct cartProduct)
        {
            try
            {
                _databaseContext.CartProducts.Add(cartProduct);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
