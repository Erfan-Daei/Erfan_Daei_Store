using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.ProductChangeDisplayed
{
    public class ChangeProductDisplayedService : IChangeProductDisplayed
    {
        private readonly IDatabaseContext _databaseContext;
        public ChangeProductDisplayedService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(long Id)
        {
            var _Product = _databaseContext.Products.Find(Id);

            if (_Product == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            _Product.Displayed = !_Product.Displayed;
            _databaseContext.SaveChanges();
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
