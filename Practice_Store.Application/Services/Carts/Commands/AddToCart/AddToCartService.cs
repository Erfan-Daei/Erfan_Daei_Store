using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Carts;

namespace Practice_Store.Application.Services.Carts.Commands.AddToCart
{
    public class AddToCartService : IAddToCart
    {
        private readonly IAddToCartRepo _addToCartRepo;
        public AddToCartService(IAddToCartRepo addToCartRepo)
        {
            _addToCartRepo = addToCartRepo;
        }
        public ResultDto AddToCart(RequestCartDto Request)
        {
            var _Cart = _addToCartRepo.GetCart(Request);

            if (_Cart == null)
            {
                Cart NewCart = new Cart()
                {
                    BrowserId = Request.BrowserId,
                    IsDone = false,
                };
                var Add = _addToCartRepo.AddCart(NewCart);
                if(Add)
                _Cart = NewCart;
            }

            var _Product = _addToCartRepo.GetProduct(Request.ProductId);
            var _CartProducts = _addToCartRepo.GetCartProducts(Request, _Cart.Id);
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
                _addToCartRepo.AddCartProduct(NewCartProduct);
            }
            _addToCartRepo.Save();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"{_Product.Name} با موفقیت به سبد خرید شما اضافه شد",
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
