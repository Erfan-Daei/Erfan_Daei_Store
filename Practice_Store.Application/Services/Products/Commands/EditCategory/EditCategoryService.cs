using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;

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

        public ResultDto Execute(RequestEditCategoryDto Request)
        {
            var _Category = _productRepoFinders.FindCategory(Request.Id);
            if (_Category == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var Edit = _editCategoryRepo.EditCategory(_Category, Request.Name);
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
