using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Common.GetProductMenu
{
    public class GetProductMenuService : IGetProductMenu
    {
        IDatabaseContext _databaseContext;
        public GetProductMenuService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<GetProductMenuDto>> Execute()
        {
            var _Categories = _databaseContext.Categories.Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new GetProductMenuDto
                {
                    CategoryId = p.Id,
                    Name = p.Name,
                    ChildCategories = p.SubCategories.ToList().Select(c => new GetProductMenuDto
                    {
                        CategoryId = c.Id,
                        Name = c.Name,
                    }).ToList(),
                }).ToList();

            return new ResultDto<List<GetProductMenuDto>>()
            {
                Data = _Categories,
                IsSuccess = true,
                Status_Code = Status_Code.OK,
            };
        }
    }
}
