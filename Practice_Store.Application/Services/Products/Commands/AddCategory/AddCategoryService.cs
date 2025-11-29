using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Commands.AddCategory
{
    public class AddCategoryService : IAddCategory
    {
        private readonly IAddCategoryRepo _addCategoryRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        public AddCategoryService(IAddCategoryRepo addCategoryRepo, IProductRepoFinders productRepoFinders)
        {
            _addCategoryRepo = addCategoryRepo;
            _productRepoFinders = productRepoFinders;
        }

        public ResultDto<long> Execute(RequestAddCategoryDto Request)
        {
            Category Category = new Category()
            {
                Name = Request.Name,
                ParentCategory = GetParent(Request.ParentId)
            };

            var result = _addCategoryRepo.AddCategory(Category);
            if (!result)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto<long>()
            {
                Data = Category.Id,
                IsSuccess = true,
                Message = "دسته بندی با موفقیت اضافه شد",
                StatusCode = StatusCodes.Status201Created,
            };

        }
        private Category? GetParent(long? ParentId)
        {
            return _productRepoFinders.FindCategory(ParentId);
        }
    }
}
