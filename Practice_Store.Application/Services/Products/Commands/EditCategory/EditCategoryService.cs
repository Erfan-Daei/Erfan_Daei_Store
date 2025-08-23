using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Products.Commands.EditCategory
{
    public class EditCategoryService : IEditCategory
    {
        private readonly IDatabaseContext _databaseContext;
        public EditCategoryService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(long Id, string Name)
        {
            var _Category = _databaseContext.Categories.Find(Id);
            if (_Category == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا نام دسته بندی را وارد نمایید",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            string NamePattern = @"^[\u0621-\u0628\u062A-\u063A\u0641-\u0642\u0644-\u0648\u064A\u067E\u0686\u0698\u06A9\u06AF\u06BE\u06CC\s]+$";
            var MatchName = Regex.Match(Name, NamePattern);
            if (!MatchName.Success)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام دسته بندی نمیتواند شامل اعداد و حروف انگلیسی باشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            _Category.Name = Name;
            _databaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
