using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetProductMenu
{
    public class GetProductMenuService : IGetProductMenu
    {
        private readonly IGetProductMenuRepo _getProductMenuRepo;
        public GetProductMenuService(IGetProductMenuRepo getProductMenuRepo)
        {
            _getProductMenuRepo = getProductMenuRepo;
        }

        public ResultDto<List<GetProductMenuDto>> Execute()
        {
            var _Categories = _getProductMenuRepo.GetCategories()
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
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
