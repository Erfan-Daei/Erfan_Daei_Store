using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public class GetProductList_AdminService : IGetProductList_Admin
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductList_AdminService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultGetProductList_AdminDto> Execute(RequestGetProductList_AdminDto Request)
        {
            var _ProductList = _databaseContext.Products
                .Include(p => p.Category)
                .Where(p => string.IsNullOrEmpty(Request.SearchKey) ||
                            p.Name.Contains(Request.SearchKey) ||
                            p.Brand.Contains(Request.SearchKey) ||
                            p.Displayed.ToString().Contains(Request.SearchKey == "نمایش" ? "true" :
                                                            Request.SearchKey == "عدم نمایش" ? "false" : Request.SearchKey) ||
                            p.Category.Name.Contains(Request.SearchKey)
                )
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.Off)
                .OrderBy(p => p.Category.ParentCategoryId)
                .ThenByDescending(p => p.Id)
                .ToPaged(Request.Page ?? 1, Request.PageSize ?? 20)
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
