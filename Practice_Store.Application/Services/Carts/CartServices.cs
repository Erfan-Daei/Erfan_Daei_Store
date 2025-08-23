using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Carts;

namespace Practice_Store.Application.Services.Carts
{
    public class CartServices : ICartServices
    {
        private readonly IDatabaseContext _databaseContext;
        public CartServices(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto AddToCart(RequestCartDto Request)
        {
            var _Cart = _databaseContext.Carts.Where(p => p.BrowserId == Request.BrowserId && !p.IsDone && (p.UserId == null || p.UserId == Request.UserId.ToString()))
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (_Cart == null)
            {
                Cart NewCart = new Cart()
                {
                    BrowserId = Request.BrowserId,
                    IsDone = false,
                };
                _databaseContext.Carts.Add(NewCart);
                _databaseContext.SaveChanges();
                _Cart = NewCart;
            }

            var _Product = _databaseContext.Products.Include(p => p.ProductSizes).Include(p => p.Off).Where(p => p.Id == Request.ProductId).FirstOrDefault();
            var _CartProducts = _databaseContext.CartProducts.Where(p => p.ProductId == Request.ProductId && p.ProductSizeId == Request.ProductSizeId && p.CartId == _Cart.Id).FirstOrDefault();
            if (_CartProducts != null)
            {
                _CartProducts.Count += Request.Count;
            }
            else
            {
                CartProduct NewCartProduct = new CartProduct()
                {
                    Cart = _Cart,
                    Count = 1,
                    Price = _Product.Price - (_Product.Price * _Product.Off?.Percentage ?? 0 / 100),
                    Product = _Product,
                    ProductSizeId = Request.ProductSizeId,
                };
                _databaseContext.CartProducts.Add(NewCartProduct);
            }
            _databaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"{_Product.Name} با موفقیت به سبد خرید شما اضافه شد",
                StatusCode = StatusCodes.Status201Created,
            };
        }

        public ResultDto<CartDto> GetCart(Guid BrowserId, string? UserId)
        {
            var _Cart = _databaseContext.Carts.Include(c => c.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductSizes)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)

                .Include(p => p.CartProducts)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Off)

                .Where(p => p.BrowserId == BrowserId && !p.IsDone && (p.UserId == null || p.UserId == UserId))
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();
            if (_Cart == null)
            {
                Cart NewCart = new Cart()
                {
                    BrowserId = BrowserId,
                    IsDone = false,
                };
                _databaseContext.Carts.Add(NewCart);
                _databaseContext.SaveChanges();
                _Cart = NewCart;
            }
            if (!string.IsNullOrEmpty(UserId) && _Cart.User == null)
            {
                var User = _databaseContext.Users.Find(UserId);
                _Cart.User = User;
                _Cart.UserId = UserId;
                _databaseContext.SaveChanges();
            }

            int sum = 0;
            if (_Cart.CartProducts?.Count != 0 && _Cart.CartProducts != null)
            {
                foreach (var item in _Cart.CartProducts)
                {
                    item.Price = item.Product.Price - ((item.Product.Price * item.Product.Off?.Percentage ?? 0) / 100);
                    _databaseContext.SaveChanges();
                    int productCount = item.Product.ProductSizes.FirstOrDefault(p => p.Id == item.ProductSizeId).Inventory;
                    if (item?.Count > productCount)
                    {
                        sum += item.Price * productCount;
                    }
                    else
                    {
                        sum += item.Price * item.Count;
                    }
                }
            }

            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    Id = _Cart.Id,
                    UserId = _Cart.UserId,
                    CartProducts = _Cart.CartProducts?.Select(p => new CartProductDto
                    {
                        Count = p.Count,
                        ProductTotalSum = p.Price,
                        ProductId = p.ProductId,
                        ProductName = p.Product.Name,
                        ProductImageSrc = p.Product.ProductImages.FirstOrDefault().Src,
                        ProductOff = p.Product.Off?.Percentage ?? 0,
                        ProductSizeId = p.ProductSizeId,
                        ProductSizeName = p.Product.ProductSizes.Where(s => s.Id == p.ProductSizeId).FirstOrDefault().Size,
                        ProductSizeInventory = p.Product.ProductSizes.Where(s => s.Id == p.ProductSizeId).FirstOrDefault().Inventory,
                        ProductPrice = p.Product.Price,
                    })?.ToList() ?? null,
                    TotalSum = sum,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public ResultDto RemoveFromCart(RequestCartDto Request)
        {
            var _CartProduct = _databaseContext.CartProducts.Where(p => p.Cart.BrowserId == Request.BrowserId && (p.Cart.UserId == null || p.Cart.UserId == Request.UserId.ToString()) && p.ProductId == Request.ProductId && p.ProductSizeId == Request.ProductSizeId)
                .FirstOrDefault();
            if (_CartProduct != null)
            {
                if (_CartProduct?.Count == 1)
                {
                    _CartProduct.IsDeleted = true;
                    _CartProduct.DeletedTime = DateTime.Now;
                }
                else
                {
                    _CartProduct.Count = _CartProduct.Count - Request.Count;
                    if (_CartProduct?.Count <= 0)
                    {
                        _CartProduct.IsDeleted = true;
                        _CartProduct.DeletedTime = DateTime.Now;
                    }
                }

                _databaseContext.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت از سبد خرید شما حذف شد",
                    StatusCode = StatusCodes.Status204NoContent,
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول در سبد خرید شما یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
        }
    }
}
