using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.ProductChangeDisplayed
{
    public class ChangeProductDisplayedService : IChangeProductDisplayed
    {
        private readonly IChangeProductDisplayRepo _changeProductDisplayRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        public ChangeProductDisplayedService(IChangeProductDisplayRepo changeProductDisplayRepo,
            IProductRepoFinders productRepoFinders)
        {
            _productRepoFinders = productRepoFinders;
            _changeProductDisplayRepo = changeProductDisplayRepo;

        }

        public ResultDto Execute(long Id)
        {
            var _Product = _productRepoFinders.FindProduct(Id);

            if (_Product == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var Change = _changeProductDisplayRepo.ChangeDisplay(_Product);
            if (!Change)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
            var State = _Product.Displayed == true ? "نمایش" : "عدم نمایش";
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"وضعیت نمایش محصول به {State} تغییر پیدا کرد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
