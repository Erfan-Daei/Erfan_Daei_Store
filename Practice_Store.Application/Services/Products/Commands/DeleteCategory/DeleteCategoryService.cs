using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Commands.DeleteCategory
{
    public class DeleteCategoryService : IDeleteCategory
    {
        private readonly IProductRepoFinders _productRepoFinders;
        private readonly IDeleteCategoryRepo _deleteCategoryRepo;
        private readonly IProductFacad _productFacad;
        public DeleteCategoryService(IProductRepoFinders productRepoFinders,
            IDeleteCategoryRepo deleteCategoryRepo,
            IProductFacad productFacad)
        {
            _deleteCategoryRepo = deleteCategoryRepo;
            _productRepoFinders = productRepoFinders;
            _productFacad = productFacad;
        }

        public ResultDto Execute(long Id)
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

            if (_Category.ParentCategoryId == null)
            {
                var SubCategoriesList = _productFacad.GetCategoriesService.Execute(Id).Data.ToList();
                var SubCategories = SubCategoriesList.Select(s => new Category
                {
                    Id = s.Id,
                }).ToList();
                var DeleteSubCateory = _deleteCategoryRepo.DeleteSubCategories(SubCategories);
                if (!DeleteSubCateory)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشل سرور",
                        StatusCode = StatusCodes.Status500InternalServerError,
                    };
                }
            }

            var DeleteCategory = _deleteCategoryRepo.DeleteCategory(_Category);
            if (!DeleteCategory)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت حذف گردید",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
