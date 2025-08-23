using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Site
{
    public class GetProductList_SiteService : IGetProductList_Site
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductList_SiteService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultGetProductList_SiteDto> Execute(RequestGetProductList_SiteDto Request, Ordering Ordering, int PageSize = 20)
        {

            var _Products = _databaseContext.Products.Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Off)
                .AsQueryable();

            if (Request.SearchKey == null && Request.CategoryId != 0)
            {
                _Products = _Products.Where(p => (p.CategoryId == Request.CategoryId ||
            p.Category.ParentCategoryId == Request.CategoryId) &&
            p.Displayed == true);
            }

            else if (Request.SearchKey != null)
            {
                if (Request.SearchKey == "Offs")
                {
                    if (Request.CategoryId == 0)
                    {
                        _Products = _Products.Where(p => p.Off.Percentage >= 30 && p.Off.Percentage <= 60);
                    }
                    else
                    {
                        _Products = _Products.Where(p => p.Off.Percentage > 0 && p.Off.Percentage <= 30);
                    }
                }
                else
                {
                    _Products = _Products.Where(p => p.Name.Contains(Request.SearchKey) ||
                    p.Brand.Contains(Request.SearchKey) ||
                    p.Category.Name.Contains(Request.SearchKey) ||
                    p.Category.ParentCategory.Name.Contains(Request.SearchKey));
                }

                if (Request.CategoryId != 0)
                {
                    _Products = _Products.Where(p => p.Category.ParentCategoryId == Request.CategoryId);
                }
            }

            else if (Request.SearchKey == null && Request.CategoryId == 0)
            {
                _Products = _Products.Where(p => p.Displayed == true);
            }

            switch (Ordering)
            {
                case Ordering.Default:
                    _Products = _Products.OrderBy(p => p.Id).AsQueryable();
                    break;
                case Ordering.Newest:
                    _Products = _Products.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostExpensive:
                    _Products = _Products.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    _Products = _Products.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.MostViewed:
                    _Products = _Products.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                default:
                    break;
            }

            int RowsCount = _Products.Count();
            var ProductList = _Products.ToPaged(Request.Page, PageSize).Select(p => new GetProductList_SiteDto
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                Price = p.Price,
                ParentCategory = p.Category.ParentCategory.Name,
                OffPercentage = p.Off?.Percentage ?? 0,
                ImageSrc = p.ProductImages.FirstOrDefault().Src,
                ProductScore = (int)Math.Floor(p.ReviewScore),
                ProductReviewCount = p.ReviewCount,
            }).ToList();

            return new ResultDto<ResultGetProductList_SiteDto>()
            {
                Data = new ResultGetProductList_SiteDto
                {
                    ProductList = ProductList,
                    CurrentPage = Request.Page,
                    PageSize = PageSize,
                    RowsCount = RowsCount
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
