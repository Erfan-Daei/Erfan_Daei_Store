using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Carts.Commands.RemoveFromCart
{
    public class RemoveFromCartService : IRemoveFromCart
    {
        private readonly IRemoveFromCartRepo _removeFromCartRepo;
        public RemoveFromCartService(IRemoveFromCartRepo removeFromCartRepo)
        {
            _removeFromCartRepo = removeFromCartRepo;
        }

        public ResultDto RemoveFromCart(RequestCartDto Request)
        {
            var _CartProduct = _removeFromCartRepo.GetCartProduct(Request);
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

                _removeFromCartRepo.Save();

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
