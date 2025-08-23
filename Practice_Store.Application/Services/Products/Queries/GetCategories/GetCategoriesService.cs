using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetCategories
{
    public class GetCategoriesService : IGetCategories
    {
        private readonly IDatabaseContext _databaseContext;
        public GetCategoriesService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public ResultDto<List<CategoriesDto>> Execute(long? ParentId)
        {
            var _Categories = _databaseContext.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId == ParentId)
                .Include(p => p.SubCategories)
                .Select(p => new CategoriesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    HasChild = p.SubCategories.Count() > 0 ? true : false,
                    Parent = p.ParentCategory != null ?
                    new ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                    } : null,
                }).ToList();

            return new ResultDto<List<CategoriesDto>>()
            {
                Data = _Categories,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
