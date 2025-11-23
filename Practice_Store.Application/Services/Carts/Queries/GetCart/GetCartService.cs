using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Quesries;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Carts;

namespace Practice_Store.Application.Services.Carts.Queries.GetCart
{
    public class GetCartService : IGetCart
    {
        private readonly IGetCartRepo _getCartRepo;
        public GetCartService(IGetCartRepo getCartRepo)
        {
            _getCartRepo = getCartRepo;
        }

        public ResultDto<CartDto> GetCart(Guid BrowserId, string? UserId)
        {
            var _Cart = _getCartRepo.GetCart(BrowserId, UserId);
            if (_Cart == null)
            {
                Cart NewCart = new Cart()
                {
                    BrowserId = BrowserId,
                    IsDone = false,
                };
                var Add = _getCartRepo.AddCart(NewCart);
                if (Add)
                    _Cart = NewCart;
            }
            if (!string.IsNullOrEmpty(UserId) && _Cart.User == null)
            {
                var User = _getCartRepo.FindUser(UserId);
                _Cart.User = User;
                _Cart.UserId = UserId;
                _getCartRepo.Save();
            }

            int sum = 0;
            if (_Cart.CartProducts?.Count != 0 && _Cart.CartProducts != null)
            {
                foreach (var item in _Cart.CartProducts)
                {
                    item.Price = item.Product.Price - ((item.Product.Price * item.Product.Off?.Percentage ?? 0) / 100);
                    _getCartRepo.Save();
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
    }
}
