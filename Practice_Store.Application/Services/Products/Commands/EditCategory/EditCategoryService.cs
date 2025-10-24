using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Products.Commands.EditCategory
{
    public class EditCategoryService : IEditCategory
    {
        private readonly IEditCategoryRepo _editCategoryRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        public EditCategoryService(IEditCategoryRepo editCategoryRepo, IProductRepoFinders productRepoFinders)
        {
            _editCategoryRepo = editCategoryRepo;
            _productRepoFinders = productRepoFinders;
        }

        public ResultDto Execute(long Id, string Name)
        {
            var _Category = _productRepoFinders.FindCategory(Id);
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

            var Edit = _editCategoryRepo.EditCategory(_Category, Name);
            if (!Edit)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
