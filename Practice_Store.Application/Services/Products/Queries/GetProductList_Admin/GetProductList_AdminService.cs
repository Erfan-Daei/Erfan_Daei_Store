using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public class GetProductList_AdminService : IGetProductList_Admin
    {
        private readonly IGetProductList_AdminRepo _getProductList_AdminRepo;
        public GetProductList_AdminService(IGetProductList_AdminRepo getProductList_AdminRepo)
        {
            _getProductList_AdminRepo = getProductList_AdminRepo;
        }

        public ResultDto<ResultGetProductList_AdminDto> Execute(RequestGetProductList_AdminDto Request)
        {
            var _ProductList = _getProductList_AdminRepo.GetProductList(Request.SearchKey, Request.Page, Request.PageSize)
                .Select(p => new ProductList_AdminDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Displayed = p.Displayed == true ? "نمایش" : "عدم نمایش",
                    Price = p.Price,
                    CategoryId = p.Category.Id,
                    CategoryName = p.Category.Name,
                    OffPercentage = p.Off?.Percentage ?? 0,
                    ProductImageSrc = p.ProductImages.FirstOrDefault(i => i.ProductId == p.Id).Src,
                    ProductScore = (int)Math.Floor(p.ReviewScore),
                    ProductReviewCount = p.ReviewCount,
                })
                .ToList();

            int RowsCount = _ProductList.Count();

            return new ResultDto<ResultGetProductList_AdminDto>
            {
                Data = new ResultGetProductList_AdminDto
                {
                    ProductList = _ProductList,
                    CurrentPage = Request.Page ?? 1,
                    PageSize = Request.PageSize ?? 20,
                    RowsCount = RowsCount
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
