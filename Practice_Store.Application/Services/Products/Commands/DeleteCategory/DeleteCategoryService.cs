using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.DeleteCategory
{
    public class DeleteCategoryService : IDeleteCategory
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IProductFacad _productFacad;
        public DeleteCategoryService(IDatabaseContext databaseContext, IProductFacad productFacad)
        {
            _databaseContext = databaseContext;
            _productFacad = productFacad;
        }

        public ResultDto Execute(long Id)
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

            if (_Category.ParentCategoryId == null)
            {
                var SubCategoriesList = _productFacad.GetCategoriesService.Execute(Id).Data;
                foreach (var Category in SubCategoriesList)
                {
                    var SubCategoryId = _databaseContext.Categories.Find(Category.Id);
                    _databaseContext.Categories.Remove(SubCategoryId);
                }
            }

            _databaseContext.Categories.Remove(_Category);
            _databaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت حذف گردید",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
